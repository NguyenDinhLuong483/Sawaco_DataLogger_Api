
namespace SawacoApi.Host.Hosting
{
    public class HostWorker : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IServiceScopeFactory _serviceScopeFactory;
        public double Lon, Lat;
        public DateTime TimeStamp;
        public bool Stolen;
        public string Bluetooth = "";
        public double Battery, Temp;

        public HostWorker(ManagedMqttClient mqttClient, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttClient = mqttClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToMqttBrokerAsync();
        }

        public async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceived;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("SAWACO/+");
        }
        private async Task OnMqttClientMessageReceived(MqttMessage arg)
        {
            var topic = arg.Topic;
            var payloadMessage = arg.Payload;

            if (topic is null || payloadMessage is null)
            {
                return;
            }

            string[] splitTopic = topic.Split('/');
            string Id = splitTopic[1];
            var metrics = JsonConvert.DeserializeObject<List<TagChangedNotification>>(payloadMessage);
            if (metrics is null)
            {
                return;
            }
            Lon = Lat = 0;
            Stolen = false;
            foreach (var metric in metrics)
            {
                metric.LoggerId = Id;
                switch (metric.Name)
                {
                    case "Longitude":
                        Lon = ParseDouble(metric.Value);
                        TimeStamp = DateTime.Parse(DateTime.UtcNow.AddHours(7).ToShortDateString() + " " + metric.Timestamp.ToString("HH:mm:ss"));
                        break;
                    case "Latitude":
                        Lat = ParseDouble(metric.Value);
                        break;
                    case "Stolen":
                        Stolen = ParseBool(metric.Value);
                        break;
                    case "Temperature":
                        Temp = ParseDouble(metric.Value);
                        break;
                    case "Bluetooth":
                        Bluetooth = metric.Value.ToString() ?? string.Empty;
                        break;
                    case "Battery":
                        Battery = ParseDouble(metric.Value);
                        break;
                }
            }

            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                var stolenLineService = scope.ServiceProvider.GetRequiredService<IStolenLineService>();
                var loggerService = scope.ServiceProvider.GetRequiredService<IGPSDeviceService>();

                var isexist = await loggerService.GetGPSDeviceById(Id);
                if(isexist is null)
                {
                    await loggerService.CreateNewGPSDevice(new AddGPSDeviceViewModel(Id, Lon, Lat, Id));
                }
                else
                {
                    if (Stolen && Lat != 0 && Lon != 0)
                    {
                        await loggerService.UpdateGPSDeviceStatus(new UpdateGPSDeviceViewModel(Lon, Lat, isexist.Name, "", Battery, Temp, true, "ON", TimeStamp), Id);
                        await stolenLineService.AddNewStolenLine(new AddStolenLineViewModel(Id, Lon, Lat, Battery, TimeStamp));
                    }
                    else if (!Stolen && Lat != 0 && Lon != 0)
                    {

                        await loggerService.UpdateGPSDeviceStatus(new UpdateGPSDeviceViewModel(Lon, Lat, isexist.Name, "", Battery, Temp, false, "ON", TimeStamp), Id);
                    }
                    else
                    {
                        await loggerService.UpdateGPSDeviceStatus(new UpdateGPSDeviceViewModel(isexist.Longitude, isexist.Latitude, isexist.Name, "", Battery, Temp, isexist.Stolen, Bluetooth, TimeStamp), Id);
                    }
                }    
            }
        }
        private double ParseDouble(object value)
        {
            return double.TryParse(value?.ToString(), out double result) ? result : 0;
        }

        private bool ParseBool(object value)
        {
            return bool.TryParse(value?.ToString(), out bool result) ? result : false;
        }


    }
}

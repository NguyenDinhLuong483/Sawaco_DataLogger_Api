
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
                var deviceService = scope.ServiceProvider.GetRequiredService<IGPSDeviceService>();

                var isexist = await deviceService.GetGPSDeviceById(Id);
                if(isexist is null)
                {
                    await deviceService.CreateNewGPSDevice(new AddGPSDeviceViewModel(Id, Lon, Lat, Id));
                }
                else
                {
                    var device = new UpdateGPSDeviceViewModel();
                    if (Stolen && Lat != 0 && Lon != 0)
                    {
                        device.HostingUpdate(Lon, Lat, Battery, Temp, true, Bluetooth, TimeStamp);
                        await deviceService.UpdateGPSDeviceStatus(device, Id);
                        await stolenLineService.AddNewStolenLine(new AddStolenLineViewModel(Id, Lon, Lat, Battery, TimeStamp));
                    }
                    else if (!Stolen && Lat != 0 && Lon != 0)
                    {
                        device.HostingUpdate(Lon, Lat, Battery, Temp, false, Bluetooth, TimeStamp);
                        await deviceService.UpdateGPSDeviceStatus(device, Id);
                    }
                    else
                    {
                        device.HostingUpdate(isexist.Longitude, isexist.Latitude, Battery, Temp, Stolen, Bluetooth, TimeStamp);
                        await deviceService.UpdateGPSDeviceStatus(device, Id);
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


namespace SawacoApi.Hubs
{
    public sealed class ScadaHost : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IHubContext<NotificationHub> _notificationHub;
        public Intrastructure.MQTTClients.Buffer _buffer;
        public double Lon, Lat;
        public DateTime TimeStamp;
        public bool Stolen = false;
        public bool Move = false;
        public string Bluetooth = "";
        public double Battery, Temp;

        public ScadaHost(ManagedMqttClient mqttClient, IHubContext<NotificationHub> notificationHub, Intrastructure.MQTTClients.Buffer buffer)
        {
            _mqttClient = mqttClient;
            _notificationHub = notificationHub;
            _buffer = buffer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToMqttBrokerAsync();
        }

        public async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceived;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("GPS/Status/+");
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
            string Id = splitTopic[2];
            var metrics = JsonConvert.DeserializeObject<List<TagChangedNotification>>(payloadMessage);
            if (metrics is null)
            {
                return;
            }
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
                    case "Bluetooth":
                        Bluetooth = metric.Value.ToString() ?? string.Empty;
                        await _notificationHub.Clients.All.SendAsync("Bluetooth", JsonConvert.SerializeObject(metric));
                        break;
                    case "Battery":
                        Battery = ParseDouble(metric.Value);
                        break;
                    case "Move":
                        Move = ParseBool(metric.Value);
                        break;
                    case "Buzzer":
                        await _notificationHub.Clients.All.SendAsync("Buzzer", JsonConvert.SerializeObject(metric));
                        break;
                }
                _buffer.Update(metric);
                var json = JsonConvert.SerializeObject(metric);
                await _notificationHub.Clients.All.SendAsync("GetAll", json);
                Console.WriteLine(json);
            }
            if (Stolen && Lat != 0 && Lon != 0)
            {
                var noti = new NotificationChanged("Vùng an toàn", $"Thiết bị {Id} rời khỏi vùng an toàn. Longitude: {Lon}; Latitude: {Lat}", TimeStamp, false);
                var json = JsonConvert.SerializeObject(noti);
                await _notificationHub.Clients.All.SendAsync($"SendNotification{Id}", json);
            }
            else if (!Stolen && Lat != 0 && Lon != 0)
            {
                var noti = new NotificationChanged("Cập nhật vị trí", $"Thiết bị {Id} cập nhật vị trí. Longitude: {Lon}; Latitude: {Lat}", TimeStamp, false);
                var json = JsonConvert.SerializeObject(noti);
                await _notificationHub.Clients.All.SendAsync($"SendNotification{Id}", json);
            }
            if (Move)
            {
                var noti = new NotificationChanged("Cảnh báo chuyển động", $"Thiết bị {Id} chuyển động.", TimeStamp, false);
                var json = JsonConvert.SerializeObject(noti);
                await _notificationHub.Clients.All.SendAsync($"SendNotification{Id}", json);
            }
            if (Battery < 20)
            {
                var noti = new NotificationChanged("Pin yếu", $"Thiết bị {Id} pin yếu. Mức pin: {Battery}", TimeStamp, false);
                var json = JsonConvert.SerializeObject(noti);
                await _notificationHub.Clients.All.SendAsync($"SendNotification{Id}", json);
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

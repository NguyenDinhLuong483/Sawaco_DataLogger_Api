
namespace SawacoApi.Host.Hosting
{
    public class HostWorker : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IServiceScopeFactory _serviceScopeFactory;
        public double Lon, Lat;
        public DateTime TimeStamp;
        public bool Stolen = false;
        public bool Move = false;
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
                    case "Bluetooth":
                        Bluetooth = metric.Value.ToString() ?? string.Empty;
                        break;
                    case "Battery":
                        Battery = ParseDouble(metric.Value);
                        break;
                    case "Move":
                        Move = ParseBool(metric.Value);
                        break;
                }
            }

            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                var stolenLineService = scope.ServiceProvider.GetRequiredService<IStolenLineService>();
                var deviceService = scope.ServiceProvider.GetRequiredService<IGPSDeviceService>();
                var objectService = scope.ServiceProvider.GetRequiredService<IGPSObjectService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                var history = scope.ServiceProvider.GetRequiredService<IHistoryService>();

                var isexist = await deviceService.IsExistDevice(Id);
                if(!isexist)
                {
                    await deviceService.CreateNewGPSDevice(new AddGPSDeviceViewModel(Id, Lon, Lat, Id));
                }
                else
                {
                    var updateDevice = new UpdateGPSDeviceViewModel();
                    var device = await deviceService.GetGPSDeviceById(Id);
                    var objectConnected = await objectService.FindObjectConnected(Id);
                    
                    if (Stolen && Lat != 0 && Lon != 0)
                    {
                        updateDevice.HostingUpdate(Lon, Lat, Battery, Temp, true, Bluetooth, TimeStamp);
                        await deviceService.UpdateGPSDeviceStatus(updateDevice, Id);
                        await history.AddBatteryHistory(new AddBatteryHistoryViewModel(Id, Battery, TimeStamp));
                        await history.AddDevicePositionHistory(new AddDevicePositionHistoryViewModel(Id, Lon, Lat, TimeStamp));
                        await history.AddObjectPositionHistory(new AddObjectPositionHistoryViewModel(objectConnected.Id, Lon, Lat, TimeStamp));
                        await stolenLineService.AddNewStolenLine(new AddStolenLineViewModel(Id, Lon, Lat, Battery, TimeStamp));
                        await notificationService.AddNewNotification(new AddNewNotificationViewModel(device.CustomerPhoneNumber, "Vùng an toàn", $"Thiết bị {Id} rời khỏi vùng an toàn. Longitude: {Lon}; Latitude: {Lat}", TimeStamp, false));
                    }
                    else if (!Stolen && Lat != 0 && Lon != 0)
                    {
                        updateDevice.HostingUpdate(Lon, Lat, Battery, Temp, false, Bluetooth, TimeStamp);
                        await deviceService.UpdateGPSDeviceStatus(updateDevice, Id);
                        await history.AddBatteryHistory(new AddBatteryHistoryViewModel(Id, Battery, TimeStamp));
                        await history.AddDevicePositionHistory(new AddDevicePositionHistoryViewModel(Id, Lon, Lat, TimeStamp));
                        await history.AddObjectPositionHistory(new AddObjectPositionHistoryViewModel(objectConnected.Id, Lon, Lat, TimeStamp));
                        await notificationService.AddNewNotification(new AddNewNotificationViewModel(device.CustomerPhoneNumber, "Cập nhật vị trí", $"Thiết bị {Id} cập nhật vị trí. Longitude: {Lon}; Latitude: {Lat}", TimeStamp, false));

                    }
                    else
                    {
                        updateDevice.HostingUpdate(device.Longitude, device.Latitude, Battery, Temp, Stolen, Bluetooth, TimeStamp);
                        await history.AddBatteryHistory(new AddBatteryHistoryViewModel(Id, Battery, TimeStamp));
                        await history.AddDevicePositionHistory(new AddDevicePositionHistoryViewModel(Id, device.Longitude, device.Latitude, TimeStamp));
                        await history.AddObjectPositionHistory(new AddObjectPositionHistoryViewModel(objectConnected.Id, device.Longitude, device.Latitude, TimeStamp));
                        await deviceService.UpdateGPSDeviceStatus(updateDevice, Id);
                    }
                    if (Move)
                    {
                        await notificationService.AddNewNotification(new AddNewNotificationViewModel(device.CustomerPhoneNumber, "Cảnh báo chuyển động", $"Thiết bị {Id} chuyển động.", TimeStamp, false));
                    }
                    if (Battery < 20)
                    {
                        await notificationService.AddNewNotification(new AddNewNotificationViewModel(device.CustomerPhoneNumber, "Pin yếu", $"Thiết bị {Id} pin yếu. Mức pin: {Battery}", TimeStamp, false));
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

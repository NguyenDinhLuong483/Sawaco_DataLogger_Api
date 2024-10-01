
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using SawacoApi.Domain.Models;
using SawacoApi.Domain.Services;
using SawacoApi.MQTTClients;
using SawacoApi.Resources.Logger;
using SawacoApi.Resources.StolenLine;
using SawacoApi.Services;

namespace SawacoApi.Hubs
{
    public sealed class ScadaHost : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IHubContext<NotificationHub> _notificationHub;
        public MQTTClients.Buffer _buffer;
        public IServiceScopeFactory _serviceScopeFactory;
        public double Lon, Lat;
        public DateTime TimeStamp;
        public bool Stolen;
        public string Bluetooth = "", Temp = "";
        public int Battery;

        public ScadaHost(ManagedMqttClient mqttClient, IHubContext<NotificationHub> notificationHub, MQTTClients.Buffer buffer, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttClient = mqttClient;
            _notificationHub = notificationHub;
            _buffer = buffer;
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
                _buffer.Update(metric);
                var json = JsonConvert.SerializeObject(metric);
                await _notificationHub.Clients.All.SendAsync("GetAll", json);
                Console.WriteLine(json);

                switch (metric.Name)
                {
                    case "Longtitude":
                        Lon = ParseDouble(metric.Value);
                        TimeStamp = DateTime.Parse(DateTime.Now.AddHours(-2).ToString("dd/MM/yyyy") +" "+ metric.Timestamp.ToString("HH:mm:ss"));
                        break;
                    case "Latitude":
                        Lat = ParseDouble(metric.Value);
                        break;
                    case "Stolen":
                        Stolen = ParseBool(metric.Value);
                        break;
                    case "Temperature":
                        Temp = metric.Value.ToString() ?? string.Empty;
                        break;
                    case "Bluetooth":
                        Bluetooth = metric.Value.ToString() ?? string.Empty;
                        break;
                    case "Battery":
                        Battery = (int)ParseDouble(metric.Value);
                        break;
                }

            }
            if (Stolen && Lat != 0 && Lon != 0)
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    IStolenLineService scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IStolenLineService>();
                    ILoggerService loggerService =
                        scope.ServiceProvider.GetRequiredService<ILoggerService>();
                    var isexist = await loggerService.GetLoggerById(Id);
                    if (isexist is not null)
                    {
                        await loggerService.UpdateLoggerStatus(new UpdateLoggerViewModel(Lon, Lat, isexist.Name, Battery, Temp, true, "ON", TimeStamp), Id);
                        await scopedProcessingService.AddNewStolenLine(new AddStolenLineViewModel(Id, Lon, Lat, Battery, TimeStamp));
                    }
                    
                }
            }
            else if (!Stolen && Lat != 0 && Lon != 0)
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    ILoggerService scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<ILoggerService>();
                    
                    try 
                    {
                        var isexist = await scopedProcessingService.GetLoggerById(Id);
                        await scopedProcessingService.UpdateLoggerStatus(new UpdateLoggerViewModel(Lon, Lat, isexist.Name, Battery, Temp, false, "ON", TimeStamp), Id);
                    }
                    catch
                    {
                        await scopedProcessingService.CreateNewLogger(new AddLoggerViewModel(Id, Lon, Lat, Id));
                    }
                }
            }
            else 
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {     
                    ILoggerService loggerService =
                        scope.ServiceProvider.GetRequiredService<ILoggerService>();
                    var isexist = await loggerService.GetLoggerById(Id);
                    
                    if (isexist is not null)
                    {
                        await loggerService.UpdateLoggerStatus(new UpdateLoggerViewModel(isexist.Longtitude, isexist.Latitude, isexist.Name, Battery, Temp, isexist.Stolen, Bluetooth, TimeStamp), Id);
                    }
                    else
                    {
                        await loggerService.CreateNewLogger(new AddLoggerViewModel(Id, Lon, Lat, Id));
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

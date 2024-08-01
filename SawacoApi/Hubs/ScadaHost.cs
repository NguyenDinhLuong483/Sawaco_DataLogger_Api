
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SawacoApi.MQTTClients;

namespace SawacoApi.Hubs
{
    public class ScadaHost : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IHubContext<NotificationHub> _notificationHub;
        public MQTTClients.Buffer _buffer;

        public ScadaHost(ManagedMqttClient mqttClient, IHubContext<NotificationHub> notificationHub, MQTTClients.Buffer buffer)
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
            await _mqttClient.Subscribe("SAWACO/STM32/+");
        }
        private async Task OnMqttClientMessageReceived(MqttMessage arg)
        {
            var topic = arg.Topic;
            var payloadMessage = arg.Payload;

            if (topic is null || payloadMessage is null)
            {
                return;
            }
            var metric = JsonConvert.DeserializeObject<TagChangedNotification>(payloadMessage);
            if (metric is null)
            {
                return;
            }
            _buffer.Update(metric);
            var json = JsonConvert.SerializeObject(metric);
            await _notificationHub.Clients.All.SendAsync("GetAll", json);
            Console.WriteLine(json);
        }
    }
}

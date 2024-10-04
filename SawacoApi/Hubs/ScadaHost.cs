
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace SawacoApi.Hubs
{
    public sealed class ScadaHost : BackgroundService
    {
        public ManagedMqttClient _mqttClient;
        public IHubContext<NotificationHub> _notificationHub;
        public Intrastructure.MQTTClients.Buffer _buffer;
        
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

            foreach (var metric in metrics)
            {
                metric.LoggerId = Id;
                _buffer.Update(metric);
                var json = JsonConvert.SerializeObject(metric);
                await _notificationHub.Clients.All.SendAsync("GetAll", json);
                Console.WriteLine(json);
            }
        }
    }
}

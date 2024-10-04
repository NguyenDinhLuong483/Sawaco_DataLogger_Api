using Microsoft.AspNetCore.SignalR;

namespace SawacoApi.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly Intrastructure.MQTTClients.Buffer _buffer;

        public NotificationHub(Intrastructure.MQTTClients.Buffer buffer)
        {
            _buffer = buffer;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("OnConnected", "Connect Successful");
        }
        public async Task SendAllAsync()
        {
            var tag = _buffer.GetAllTags();
            await Clients.All.SendAsync("SendAll", tag);
        }
    }
}

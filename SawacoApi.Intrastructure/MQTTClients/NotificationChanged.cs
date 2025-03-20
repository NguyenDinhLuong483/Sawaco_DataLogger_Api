
namespace SawacoApi.Intrastructure.MQTTClients
{
    public class NotificationChanged
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAcknowledge { get; set; }
        public NotificationChanged(string title, string description, DateTime timestamp, bool isAcknowledge)
        {
            Title = title;
            Description = description;
            Timestamp = timestamp;
            IsAcknowledge = isAcknowledge;
        }
    }
}

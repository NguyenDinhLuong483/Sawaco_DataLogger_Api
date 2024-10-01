namespace SawacoApi.MQTTClients
{
    public class TagChangedNotification
    {
        public string LoggerId {  get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }

        public TagChangedNotification(string id, string name, object value, DateTime timestamp)
        {
            LoggerId = id;
            Name = name;
            Value = value;
            Timestamp = timestamp;
        }
    }
}

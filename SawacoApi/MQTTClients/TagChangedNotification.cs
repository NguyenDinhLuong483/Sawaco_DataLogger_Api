namespace SawacoApi.MQTTClients
{
    public class TagChangedNotification
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }

        public TagChangedNotification(string name, object value, DateTime timestamp)
        {
            Name = name;
            Value = value;
            Timestamp = timestamp;
        }
    }
}

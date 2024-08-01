using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SawacoApi.Resources.Logger
{
    [DataContract]
    public class AddLoggerViewModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        [JsonIgnore]
        public int Battery { get; set; }
        [DataMember]
        [JsonIgnore]
        public bool Stolen { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Bluetooth { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }

        public AddLoggerViewModel(string id, double longtitude, double latitude, string name)
        {
            Id = id;
            Longtitude = longtitude;
            Latitude = latitude;
            Name = name;
            Battery = 100;
            Stolen = false;
            Bluetooth = "ON";
            TimeStamp = DateTime.Now;
        }
    }
}

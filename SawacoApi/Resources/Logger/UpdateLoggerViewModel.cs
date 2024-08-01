
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SawacoApi.Resources.Logger
{
    [DataContract]
    public class UpdateLoggerViewModel
    {
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Battery { get; set; }
        [DataMember]
        public bool Stolen { get; set; }
        [DataMember]
        public string Bluetooth { get; set; }
        [DataMember]
        public DateTime TimeStamp {  get; set; }

        public UpdateLoggerViewModel(double longtitude, double latitude, string name, int battery, bool stolen, string bluetooth, DateTime timeStamp)
        {
            Longtitude = longtitude;
            Latitude = latitude;
            Name = name;
            Battery = battery;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
        }
    }
}

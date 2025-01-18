
namespace SawacoApi.Intrastructure.ViewModel.GPSDevices
{
    [DataContract]
    public class AddGPSDeviceViewModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        [JsonIgnore]
        public string CustomerPhoneNumber { get; set; }
        [DataMember]
        [JsonIgnore]
        public string ImagePath { get; set; }
        [DataMember]
        [JsonIgnore]
        public double Battery { get; set; }
        [DataMember]
        [JsonIgnore]
        public double Temperature { get; set; }
        [DataMember]
        [JsonIgnore]
        public bool Stolen { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Bluetooth { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        [JsonIgnore]
        public string SMSNumber { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Package { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime RegistationDate { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime ExpirationDate { get; set; }

        public AddGPSDeviceViewModel(string id, double longitude, double latitude, string name)
        {
            Id = id;
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            ImagePath = Bluetooth = Package = SMSNumber = "";
            CustomerPhoneNumber = "NSX";
            Battery = 0;
            Temperature = 0;
            Stolen = false;
            TimeStamp = RegistationDate = ExpirationDate = DateTime.MinValue;
        }
    }
}

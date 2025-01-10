
namespace SawacoApi.Intrastructure.ViewModel.GPSDevices
{
    public class AddGPSDeviceViewModel
    {
        public string Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string CustomerPhoneNumber { get; set; }
        [JsonIgnore]
        public string ImagePath { get; set; }
        [JsonIgnore]
        public double Battery { get; set; }
        [JsonIgnore]
        public double Temperature { get; set; }
        [JsonIgnore]
        public bool Stolen { get; set; }
        [JsonIgnore]
        public string Bluetooth { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
        [JsonIgnore]
        public string SMSNumber { get; set; }
        [JsonIgnore]
        public string Package { get; set; }
        [JsonIgnore]
        public DateTime RegistationDate { get; set; }
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
            Battery = 0.0;
            Temperature = 0.0;
            Stolen = false;
            TimeStamp = RegistationDate = ExpirationDate = DateTime.MinValue;
        }
    }
}

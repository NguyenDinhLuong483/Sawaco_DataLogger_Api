namespace SawacoApi.Intrastructure.ViewModel.GPSDevices
{
    public class UpdateGPSDeviceViewModel
    {
        public string CustomerPhoneNumber { get; set; } = "";
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public double Battery { get; set; }
        public double Temperature { get; set; }
        public bool Stolen { get; set; }
        public string Bluetooth { get; set; } = "";
        public DateTime TimeStamp { get; set; }
        public string SMSNumber { get; set; } = "";
        public string Package { get; set; } = "";   
        public DateTime RegistationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void HostingUpdate(double longitude, double latitude, double battery, double temperature, bool stolen, string bluetooth, DateTime timeStamp)
        {
            Longitude = longitude;
            Latitude = latitude;
            Battery = battery;
            Temperature = temperature;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
        }
    }
}

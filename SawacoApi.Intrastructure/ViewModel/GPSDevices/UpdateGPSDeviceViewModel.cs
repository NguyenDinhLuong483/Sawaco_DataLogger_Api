namespace SawacoApi.Intrastructure.ViewModel.GPSDevices
{
    public class UpdateGPSDeviceViewModel
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Battery { get; set; }
        public double Temperature { get; set; }
        public bool Stolen { get; set; }
        public string Bluetooth { get; set; }
        public DateTime TimeStamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public UpdateGPSDeviceViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public UpdateGPSDeviceViewModel(double longitude, double latitude, string name, string imagePath, double battery, double temperature, bool stolen, string bluetooth, DateTime timeStamp)
        {
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            ImagePath = imagePath;
            Battery = battery;
            Temperature = temperature;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
        }
    }
}


namespace SawacoApi.Intrastructure.ViewModel.GPSObjects
{
    public class GPSObjectViewModel
    {
        public string Id { get; set; }
        public string GPSDeviceId { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int SafeRadius { get; set; }
        public string Size { get; set; }
        public bool Connected { get; set; }

        public GPSObjectViewModel(string id, string gPSDeviceId, string name, double longitude, double latitude, string description, string imagePath, int safeRadius, string size, bool connected)
        {
            Id = id;
            GPSDeviceId = gPSDeviceId;
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            Description = description;
            ImagePath = imagePath;
            SafeRadius = safeRadius;
            Size = size;
            Connected = connected;
        }
    }
}


namespace SawacoApi.Intrastructure.ViewModel.GPSObjects
{
    [DataContract]
    public class CreateNewObjectViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        [JsonIgnore]
        public string GPSDeviceId { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int SafeRadius { get; set; }
        public string Size { get; set; }
        [JsonIgnore]
        public bool Connected { get; set; }

        public CreateNewObjectViewModel(string name, double longitude, double latitude, string description, string imagePath, int safeRadius, string size, string customerPhoneNumber)
        {
            Id = "";
            GPSDeviceId = "";
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            Description = description;
            ImagePath = imagePath;
            SafeRadius = safeRadius;
            Size = size;
            Connected = false;
            CustomerPhoneNumber = customerPhoneNumber;
        }
    }
}

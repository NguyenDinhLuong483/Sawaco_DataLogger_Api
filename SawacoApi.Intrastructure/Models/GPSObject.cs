
namespace SawacoApi.Intrastructure.Models
{
    public class GPSObject
    {
        public string Id { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string GPSDeviceId { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description {  get; set; }
        public string ImagePath {  get; set; }
        public int SafeRadius {  get; set; }
        public string Size {  get; set; }
        public bool Connected { get; set; }
        public Customer Customer { get; set; }
        public List<ObjectPositionHistory> ObjectPositionHistories { get; set; }
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public GPSObject(){}
        public GPSObject(string id, string customerPhoneNumber, string gPSDeviceId, string name, string description, string imagePath, int safeRadius, string size, double longitude, double latitude)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            Id = id;
            CustomerPhoneNumber = customerPhoneNumber;
            GPSDeviceId = gPSDeviceId;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            SafeRadius = safeRadius;
            Size = size;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}

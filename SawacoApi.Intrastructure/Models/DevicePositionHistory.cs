
namespace SawacoApi.Intrastructure.Models
{
    public class DevicePositionHistory
    {
        public int Id { get; set; }
        public string GPSDeviceId {  get; set; }
        public double Longitude {  get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }
        public GPSDevice GPSDevice { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public DevicePositionHistory()
        {
        }

        public DevicePositionHistory(int id, string gPSDeviceId, double longitude, double latitude, DateTime timestamp)
        {
            Id = id;
            GPSDeviceId = gPSDeviceId;
            Longitude = longitude;
            Latitude = latitude;
            Timestamp = timestamp;
        }
    }
}

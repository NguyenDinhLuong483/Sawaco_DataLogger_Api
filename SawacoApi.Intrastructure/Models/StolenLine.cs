
namespace SawacoApi.Intrastructure.Models
{
    public class StolenLine
    {
        public int Id { get; set; }
        public string GPSDeviceId { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public double Battery { get; set; }
        public DateTime TimeStamp { get; set; }
        public GPSDevice GPSDevice { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StolenLine()

        {
        }

        public StolenLine(string gpsDeviceId, double longtitude, double latitude, double battery, DateTime timeStamp)
        {
            GPSDeviceId = gpsDeviceId;
            Longtitude = longtitude;
            Latitude = latitude;
            TimeStamp = timeStamp;
            Battery = battery;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

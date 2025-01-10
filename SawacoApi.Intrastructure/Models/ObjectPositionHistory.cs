
namespace SawacoApi.Intrastructure.Models
{
    public class ObjectPositionHistory
    {
        public int Id { get; set; }
        public string GPSObjectId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }
        public GPSObject GPSObject { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ObjectPositionHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public ObjectPositionHistory(int id, string gPSObjectId, double longitude, double latitude, DateTime timestamp, GPSObject gPSObject)
        {
            Id = id;
            GPSObjectId = gPSObjectId;
            Longitude = longitude;
            Latitude = latitude;
            Timestamp = timestamp;
            GPSObject = gPSObject;
        }
    }
}

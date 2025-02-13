
namespace SawacoApi.Intrastructure.Models
{
    public class BatteryHistory
    {
        public string Id {  get; set; }
        public string GPSDeviceId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BatteryHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public BatteryHistory(string id,  double value, DateTime timestamp, string gPSDeviceId)
        {
            Id = id;
            Value = value;
            Timestamp = timestamp;
            GPSDeviceId = gPSDeviceId;
        }
    }
}


namespace SawacoApi.Intrastructure.ViewModel.Histories
{
    public class AddBatteryHistoryViewModel
    {
        public string GPSDeviceId {  get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }

        public AddBatteryHistoryViewModel (string gpsdeviceId, double value, DateTime timestamp)
        {
            GPSDeviceId = gpsdeviceId;
            Value = value;
            Timestamp = timestamp;
        }
    }
}

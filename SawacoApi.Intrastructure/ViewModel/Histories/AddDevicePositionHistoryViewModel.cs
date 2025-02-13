
namespace SawacoApi.Intrastructure.ViewModel.Histories
{
    public class AddDevicePositionHistoryViewModel
    {
        public string GPSDeviceId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }

        public AddDevicePositionHistoryViewModel(string gPSDeviceId, double longitude, double latitude, DateTime timestamp)
        {
            GPSDeviceId = gPSDeviceId;
            Longitude = longitude;
            Latitude = latitude;
            Timestamp = timestamp;
        }
    }
}

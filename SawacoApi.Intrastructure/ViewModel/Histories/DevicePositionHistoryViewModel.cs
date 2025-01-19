
namespace SawacoApi.Intrastructure.ViewModel.Histories
{
    public class DevicePositionHistoryViewModel
    {
        public string GPSDeviceId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

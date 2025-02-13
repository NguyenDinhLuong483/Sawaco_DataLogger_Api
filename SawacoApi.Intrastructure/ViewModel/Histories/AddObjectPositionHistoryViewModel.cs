
namespace SawacoApi.Intrastructure.ViewModel.Histories
{
    public class AddObjectPositionHistoryViewModel
    {
        public string GPSObjectId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }

        public AddObjectPositionHistoryViewModel(string gPSObjectId, double longitude, double latitude, DateTime timestamp)
        {
            GPSObjectId = gPSObjectId;
            Longitude = longitude;
            Latitude = latitude;
            Timestamp = timestamp;
        }
    }
}

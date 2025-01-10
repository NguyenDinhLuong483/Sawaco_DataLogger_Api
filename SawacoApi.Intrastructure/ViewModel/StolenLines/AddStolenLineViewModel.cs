
namespace SawacoApi.Intrastructure.ViewModel.StolenLines
{
    [DataContract]
    public class AddStolenLineViewModel
    {
        [DataMember]
        public string GPSDeviceId { get; set; }
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Battery { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }

        public AddStolenLineViewModel(string gPSDeviceId, double longtitude, double latitude, double battery, DateTime timestamp)
        {
            GPSDeviceId = gPSDeviceId;
            Longtitude = longtitude;
            Latitude = latitude;
            Battery = battery;
            Timestamp = timestamp;
        }
    }
}

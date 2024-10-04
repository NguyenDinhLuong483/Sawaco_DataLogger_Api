
namespace SawacoApi.Intrastructure.ViewModel.StolenLine
{
    [DataContract]
    public class AddStolenLineViewModel
    {
        [DataMember]
        public string LoggerId { get; set; }
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public int Battery { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }

        public AddStolenLineViewModel(string loggerId, double longtitude, double latitude, int battery, DateTime timestamp)
        {
            LoggerId = loggerId;
            Longtitude = longtitude;
            Latitude = latitude;
            Battery = battery;
            Timestamp = timestamp;
        }
    }
}

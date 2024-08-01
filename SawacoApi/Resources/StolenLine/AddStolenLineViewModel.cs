using System.Runtime.Serialization;

namespace SawacoApi.Resources.StolenLine
{
    [DataContract]
    public class AddStolenLineViewModel
    {
        [DataMember]
        public string LoggerId {  get; set; }
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }

        public AddStolenLineViewModel(string loggerId, double longtitude, double latitude, DateTime dateTime)
        {
            LoggerId = loggerId;
            Longtitude = longtitude;
            Latitude = latitude;
            DateTime = dateTime;
        }
    }
}

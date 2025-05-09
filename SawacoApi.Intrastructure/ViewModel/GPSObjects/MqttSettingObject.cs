
namespace SawacoApi.Intrastructure.ViewModel.GPSObjects
{
    public class MqttSettingObject
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SafeRadius { get; set; }
        public DateTime CurrentTime { get; set; }
        public DateTime AlarmTime { get; set; }
        public string BlueTooth { get; set; } = "";
        public string Buzzer { get; set; } = "";
        public bool Emergency {  get; set; }
        public string PhoneNumber { get; set; } = "";
        public bool Sleep { get; set; } 
        public double Threshold { get; set; }
    }
}


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
    }
}

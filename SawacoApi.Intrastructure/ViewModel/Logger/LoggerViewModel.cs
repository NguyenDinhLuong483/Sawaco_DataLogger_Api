
namespace SawacoApi.Intrastructure.ViewModel.Logger
{
    public class LoggerViewModel
    {
        public string Id { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Temperature { get; set; }
        public int Battery { get; set; }
        public bool Stolen { get; set; }
        public string Bluetooth { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<StolenLineViewModel> StolenLines { get; set; }
    }
}

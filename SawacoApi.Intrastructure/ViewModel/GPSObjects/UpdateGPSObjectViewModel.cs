
namespace SawacoApi.Intrastructure.ViewModel.GPSObjects
{
    public class UpdateGPSObjectViewModel
    {
        public string CustomerPhoneNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public int SafeRadius { get; set; }
        public string Size { get; set; } = "";
    }
}

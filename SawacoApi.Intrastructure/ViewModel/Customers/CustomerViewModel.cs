
namespace SawacoApi.Intrastructure.ViewModel.Customers
{
    public class CustomerViewModel
    {
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<DeviceIdViewModel> DeviceIds { get; set; }
        public List<ObjectNameViewModel> ObjectNames { get; set; }
    }
}

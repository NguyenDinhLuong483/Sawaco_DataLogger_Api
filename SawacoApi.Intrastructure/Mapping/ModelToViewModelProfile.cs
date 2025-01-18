
namespace SawacoApi.Intrastructure.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<GPSDevice, GPSDeviceViewModel>();
            CreateMap<StolenLine, StolenLineViewModel>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<GPSDevice, DeviceIdViewModel>();
            CreateMap<GPSObject, ObjectNameViewModel>();
            CreateMap<GPSObject, GPSObjectViewModel>();

            CreateMap<AddStolenLineViewModel, StolenLine>();
            CreateMap<AddNewCustomerViewModel,  Customer>();
            CreateMap<AddGPSDeviceViewModel, GPSDevice>();
            CreateMap<CreateNewObjectViewModel, GPSObject>();
        }
    }
}

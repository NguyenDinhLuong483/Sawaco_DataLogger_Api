using SawacoApi.Intrastructure.ViewModel.Logger;

namespace SawacoApi.Intrastructure.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<Logger, LoggerViewModel>();
            CreateMap<AddLoggerViewModel, Logger>();
            CreateMap<UpdateLoggerViewModel, Logger>();
            CreateMap<StolenLine, StolenLineViewModel>();
            CreateMap<AddStolenLineViewModel, StolenLine>();
        }
    }
}

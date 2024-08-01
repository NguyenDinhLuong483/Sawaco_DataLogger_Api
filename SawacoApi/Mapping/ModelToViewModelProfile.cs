using AutoMapper;
using SawacoApi.Domain.Models;
using SawacoApi.Resources.Logger;
using SawacoApi.Resources.StolenLine;

namespace SawacoApi.Mapping
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


namespace SawacoApi.Intrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        public ILoggerRepository _loggerRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public LoggerService(ILoggerRepository loggerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _loggerRepository = loggerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LoggerViewModel>> GetLogger()
        {
            var source = await _loggerRepository.GetAllLoggerAsync() ?? throw new ResourceNotFoundException();
            var viewmodel = _mapper.Map<List<Logger>, List<LoggerViewModel>>(source);
            return viewmodel;
        }

        public async Task<LoggerViewModel> GetLoggerById(string id)
        {
            var source = await _loggerRepository.GetLoggerByIdAsync(id);
            var viewmodel = _mapper.Map<LoggerViewModel>(source);
            return viewmodel;
        }

        public async Task<bool> CreateNewLogger(AddLoggerViewModel addLogger)
        {
            var mapping = _mapper.Map<Logger>(addLogger);
            var IsExist = await _loggerRepository.GetLoggerByIdAsync(mapping.Id);
            if (IsExist is not null)
            {
                throw new EntityDuplicationException("This logger is existing!");
            }
            var newlogger = _loggerRepository.CreateLoggerAsync(mapping);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteLogger(string loggerId)
        {
            var IsExist = await _loggerRepository.GetLoggerByIdAsync(loggerId) ?? throw new ResourceNotFoundException("Not found this Looger!");
            _loggerRepository.DeleteLoggerAsync(IsExist);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateLoggerStatus(UpdateLoggerViewModel updateLogger, string loggerId)
        {
            var IsExist = await _loggerRepository.GetLoggerByIdAsync(loggerId) ?? throw new ResourceNotFoundException();
            IsExist.UpdateAll(updateLogger.Longtitude, updateLogger.Latitude, updateLogger.Name, updateLogger.Battery, updateLogger.Temperature, updateLogger.Stolen, updateLogger.Bluetooth, updateLogger.TimeStamp);
            var update = _loggerRepository.UpdateLoggerAsync(IsExist);
            return await _unitOfWork.CompleteAsync();
        }
    }
}

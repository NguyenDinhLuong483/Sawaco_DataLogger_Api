
namespace SawacoApi.Intrastructure.Services
{
    public interface ILoggerService
    {
        public Task<List<LoggerViewModel>> GetLogger();
        public Task<LoggerViewModel> GetLoggerById(string id);
        public Task<bool> CreateNewLogger(AddLoggerViewModel addLogger);
        public Task<bool> DeleteLogger(string loggerId);
        public Task<bool> UpdateLoggerStatus(UpdateLoggerViewModel updateLogger, string loggerId);
    }
}

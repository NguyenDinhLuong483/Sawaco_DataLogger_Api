using SawacoApi.Domain.Models;
using SawacoApi.Resources;

namespace SawacoApi.Domain.Repositories
{
    public interface ILoggerRepository
    {
        public Task<List<Logger>> GetAllLoggerAsync();
        public Task<Logger> GetLoggerByIdAsync(string id); 
        public Logger CreateLoggerAsync(Logger newlogger);
        public bool DeleteLoggerAsync(Logger deleteLogger);
        public bool UpdateLoggerAsync(Logger newlogger);
    }
}

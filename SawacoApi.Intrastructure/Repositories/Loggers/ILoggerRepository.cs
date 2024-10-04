using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawacoApi.Intrastructure.Repositories.Loggers
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

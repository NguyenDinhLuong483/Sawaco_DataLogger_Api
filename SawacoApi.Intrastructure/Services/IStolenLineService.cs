using SawacoApi.Intrastructure.ViewModel.StolenLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawacoApi.Intrastructure.Services
{
    public interface IStolenLineService
    {
        public Task<List<StolenLineViewModel>> GetByLoggerId(string loggerId);
        public Task<List<StolenLineViewModel>> GetByDate(string id, DateTime startDate, DateTime endDate);
        public Task<bool> DeleteByLoggerId(string loggerId);
        public Task<bool> DeleteByDate(string id, DateTime startDate, DateTime endDate);
        public Task<bool> AddNewStolenLine(AddStolenLineViewModel stolenLine);
    }
}

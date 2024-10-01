using Microsoft.EntityFrameworkCore.Query.Internal;
using SawacoApi.Resources.StolenLine;

namespace SawacoApi.Domain.Services
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

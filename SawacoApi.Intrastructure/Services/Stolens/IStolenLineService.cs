
namespace SawacoApi.Intrastructure.Services.Stolens
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

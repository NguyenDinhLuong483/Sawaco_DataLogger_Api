
namespace SawacoApi.Intrastructure.Repositories.StolenLines
{
    public interface IStolenLineRepository
    {
        public Task<List<StolenLine>> GetByDeviceIdAsync(string loggerId);
        public Task<List<StolenLine>> GetByDateAsync(string id, DateTime startDate, DateTime endDate);
        public bool DeleteAsync(List<StolenLine> stolenLine);
        public StolenLine AddStolenLine(StolenLine stolenLine);
    }
}

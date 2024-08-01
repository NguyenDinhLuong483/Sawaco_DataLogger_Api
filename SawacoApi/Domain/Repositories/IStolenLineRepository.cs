using SawacoApi.Domain.Models;

namespace SawacoApi.Domain.Repositories
{
    public interface IStolenLineRepository
    {
        public Task<List<StolenLine>> GerByLoggerIdAsync(string loggerId);
        public bool DeleteByLoggerIdAsync(List<StolenLine> stolenLine);
        public StolenLine AddStolenLine(StolenLine stolenLine);
    }
}

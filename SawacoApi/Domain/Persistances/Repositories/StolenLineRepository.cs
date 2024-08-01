using Microsoft.EntityFrameworkCore;
using SawacoApi.Domain.Models;
using SawacoApi.Domain.Persistances.Context;
using SawacoApi.Domain.Repositories;

namespace SawacoApi.Domain.Persistances.Repositories
{
    public class StolenLineRepository : BaseRepository, IStolenLineRepository
    {
        public StolenLineRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<StolenLine>> GerByLoggerIdAsync(string loggerId)
        {
            return await _context.StolenLines.OrderBy(x => x.LoggerId == loggerId).ToListAsync();
        }
        public bool DeleteByLoggerIdAsync(List<StolenLine> stolenLine)
        {
            _context.StolenLines.RemoveRange(stolenLine);
            return true;
        }

        public StolenLine AddStolenLine(StolenLine stolenLine)
        {
            return _context.StolenLines.Add(stolenLine).Entity;
            
        }
    }
}

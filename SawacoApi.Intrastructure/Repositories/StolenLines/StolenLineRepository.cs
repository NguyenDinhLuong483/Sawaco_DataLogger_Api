﻿
namespace SawacoApi.Intrastructure.Repositories.StolenLines
{
    public class StolenLineRepository : BaseRepository, IStolenLineRepository
    {
        public StolenLineRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<StolenLine>> GetByDeviceIdAsync(string loggerId)
        {
            return await _context.StolenLines.OrderBy(x => x.GPSDeviceId == loggerId).ToListAsync();
        }

        public async Task<List<StolenLine>> GetByDateAsync(string id, DateTime startDate, DateTime endDate)
        {
            return await _context.StolenLines.Where(x => x.GPSDeviceId == id && x.TimeStamp >= startDate && x.TimeStamp <= endDate).ToListAsync();
        }

        public bool DeleteAsync(List<StolenLine> stolenLine)
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

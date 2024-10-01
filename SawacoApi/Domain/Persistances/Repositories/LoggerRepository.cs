using Microsoft.EntityFrameworkCore;
using SawacoApi.Domain.Models;
using SawacoApi.Domain.Persistances.Context;
using SawacoApi.Domain.Persistances.Exceptions;
using SawacoApi.Domain.Repositories;
using SawacoApi.Resources;

namespace SawacoApi.Domain.Persistances.Repositories
{
    public class LoggerRepository : BaseRepository, ILoggerRepository
    {
        public LoggerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Logger>> GetAllLoggerAsync()
        {
            return await _context.Loggers.Include(x => x.StolenLines).ToListAsync();
        }

        public async Task<Logger> GetLoggerByIdAsync(string id)
        {
            return await _context.Loggers.Include(x => x.StolenLines).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Logger CreateLoggerAsync(Logger newlogger)
        {
            if (newlogger.Id == "")
            {
                throw new ResourceNotFoundException("Imposible create this id!");
            }
            else
            {
                return _context.Loggers.Add(newlogger).Entity;
            }
        }

        public bool DeleteLoggerAsync(Logger deleteLogger)
        {
            _context.Loggers.RemoveRange(deleteLogger);
            return true;
        }

        public bool UpdateLoggerAsync(Logger newlogger)
        {
            _context.Update(newlogger);
            return true;
        }
    }
}

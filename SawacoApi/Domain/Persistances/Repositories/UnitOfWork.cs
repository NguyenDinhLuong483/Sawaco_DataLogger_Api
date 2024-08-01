using SawacoApi.Domain.Persistances.Context;
using SawacoApi.Domain.Repositories;

namespace SawacoApi.Domain.Persistances.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CompleteAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

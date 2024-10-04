
namespace SawacoApi.Intrastructure.Repositories.UnitOfWork
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

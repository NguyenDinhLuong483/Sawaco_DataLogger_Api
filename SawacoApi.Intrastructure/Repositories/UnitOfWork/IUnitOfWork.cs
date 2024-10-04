
namespace SawacoApi.Intrastructure.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<bool> CompleteAsync();
    }
}

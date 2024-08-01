namespace SawacoApi.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task<bool> CompleteAsync();
    }
}

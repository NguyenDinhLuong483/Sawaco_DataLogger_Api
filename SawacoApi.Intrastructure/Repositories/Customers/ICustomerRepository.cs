
namespace SawacoApi.Intrastructure.Repositories.Customers
{
    public interface ICustomerRepository
    {
        public Task<Customer> RegisterNewCustomer(Customer customer);
        public Task<bool> IsExistCustomer(string customerPhoneNumber);
        public Task<List<Customer>> GetAllCustomerAsync();
        public Task<Customer> GetCustomerByPhoneNumberAsync(string phoneNumber);
        public Task<bool> IdentifyCustomer(string phoneNumber, string password);
        public Task UpdateInformation(Customer customer);
        public Task DeleteCustomer(Customer customer);
    }
}

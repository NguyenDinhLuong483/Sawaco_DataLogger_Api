
namespace SawacoApi.Intrastructure.Repositories.Customers
{
    public interface ICustomerRepository
    {
        public Task<Customer> RegisterNewCustomer(Customer customer);
        public Task<bool> IsExistCustomer(string customerPhoneNumber);
        public Task<List<Customer>> GetAllCustomerAsync();
        public Task<Customer> GetCustomerByPhoneNumberAsync(string phoneNumber);
        public Task<bool> IdentifyCustomer(string phoneNumber, string password);
        public Task UpdatePhoneNumber(Customer customer);
        public Task UpdateInformation(Customer customer);
        public Task UpdateDevice(List<GPSDevice> devices);
        public Task UpdateObject(List<GPSObject> objects);
        public Task DeleteCustomer(Customer customer);
        public Task<List<GPSDevice>> GetGPSDeviceAsync(string phoneNumber);
        public Task<List<GPSObject>> GetGPSObjectAsync(string phoneNumber);
        public Task<Customer> LoginAsync(LoginViewModel user);
    }
}


namespace SawacoApi.Intrastructure.Repositories.Customers
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteCustomer(Customer customer)
        {
            _context.Customers.RemoveRange(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            return await _context.Customers.Include(x => x.GPSDevices).Include(x => x.GPSObjects).ToListAsync();
        }

        public async Task<Customer> GetCustomerByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Customers.FindAsync(phoneNumber) ?? throw new ResourceNotFoundException("Not found this customer!");
        }

        public async Task<bool> IdentifyCustomer(string phoneNumber, string password)
        {
            return await _context.Customers.AnyAsync(x => x.PhoneNumber == phoneNumber && x.Password == password);
        }

        public async Task<bool> IsExistCustomer(string customerPhoneNumber)
        {
            return await _context.Customers.AnyAsync(x => x.PhoneNumber == customerPhoneNumber);
        }

        public async Task<Customer> RegisterNewCustomer(Customer customer)
        {
            var customerEntry = await _context.Customers.AddAsync(customer);
            return customerEntry.Entity;
        }

        public async Task UpdateInformation(Customer customer)
        {
            _context.Customers.Update(customer); 
            await _context.SaveChangesAsync();
        }
    }
}

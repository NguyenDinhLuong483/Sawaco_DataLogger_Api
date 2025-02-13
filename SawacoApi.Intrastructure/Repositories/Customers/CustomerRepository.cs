
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
            return await _context.Customers.Include(x => x.GPSDevices).Include(x => x.GPSObjects).FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) ?? throw new ResourceNotFoundException("Not found this customer!");
        }

        public async Task<List<GPSDevice>> GetGPSDeviceAsync(string phoneNumber)
        {
            return await _context.GPSDevices.Where(x => x.CustomerPhoneNumber == phoneNumber).ToListAsync(); 
        }

        public async Task<List<GPSObject>> GetGPSObjectAsync(string phoneNumber)
        {
            return await _context.GPSObjects.Where(x => x.CustomerPhoneNumber == phoneNumber).ToListAsync();
        }

        public async Task<bool> IdentifyCustomer(string phoneNumber, string password)
        {
            return await _context.Customers.AnyAsync(x => x.PhoneNumber == phoneNumber && x.Password == password);
        }

        public async Task<bool> IsExistCustomer(string customerPhoneNumber)
        {
            return await _context.Customers.AnyAsync(x => x.PhoneNumber == customerPhoneNumber);
        }

        public async Task<Customer> LoginAsync(LoginViewModel user)
        {
            var currentUser = await _context.Customers.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password);
            return currentUser != null ? currentUser : throw new ResourceNotFoundException("UserName or Password is incorrect!");
        }

        public async Task<Customer> RegisterNewCustomer(Customer customer)
        {
            var customerEntry = await _context.Customers.AddAsync(customer);
            return customerEntry.Entity;
        }

        public async Task UpdateDevice(List<GPSDevice> devices)
        {
            _context.GPSDevices.UpdateRange(devices);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInformation(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateObject(List<GPSObject> objects)
        {
            _context.GPSObjects.UpdateRange(objects);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhoneNumber(Customer customer)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}

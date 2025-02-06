
namespace SawacoApi.Intrastructure.Repositories.Notifications
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddNewNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task<List<Notification>> GetNotificationByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Notifications.Where(x => x.CustomerPhoneNumber == phoneNumber).ToListAsync();
        }

        public async Task<bool> IsExistCustomer(string phoneNumber)
        {
            return await _context.Customers.AnyAsync(customer => customer.PhoneNumber == phoneNumber);
        }
    }
}

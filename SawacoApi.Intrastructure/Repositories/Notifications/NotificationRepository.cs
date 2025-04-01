
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

        public async Task DeleteNotification(List<Notification> notification)
        {
            _context.Notifications.RemoveRange(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationByPhoneNumber(string phoneNumber)
        {
            return await _context.Notifications.Where(x => x.CustomerPhoneNumber == phoneNumber && !x.IsAcknowledge).ToListAsync();
        }

        public async Task<List<Notification>> GetNotificationByPhoneNumberAndDateRange(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            return await _context.Notifications.Where(x => x.CustomerPhoneNumber == phoneNumber && x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }

        public async Task<Notification> GetNotificationByPhoneNumberAndTimestamp(string phoneNumber, string title, DateTime timestamp)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x => x.CustomerPhoneNumber == phoneNumber && x.Title == title && x.Timestamp == timestamp);
        }

        public async Task<List<Notification>> GetNotificationByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Notifications.Where(x => x.CustomerPhoneNumber == phoneNumber).ToListAsync();
        }

        public async Task<bool> IsExistCustomer(string phoneNumber)
        {
            return await _context.Customers.AnyAsync(customer => customer.PhoneNumber == phoneNumber);
        }

        public async Task UpdateMultiNotification(List<Notification> notifications)
        {
            _context.Notifications.UpdateRange(notifications);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }
    }
}

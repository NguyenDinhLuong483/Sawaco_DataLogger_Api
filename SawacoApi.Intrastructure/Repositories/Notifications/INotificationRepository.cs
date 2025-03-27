
namespace SawacoApi.Intrastructure.Repositories.Notifications
{
    public interface INotificationRepository
    {
        public Task AddNewNotificationAsync(Notification notification);
        public Task<bool> IsExistCustomer(string phoneNumber);
        public Task<List<Notification>> GetNotificationByPhoneNumberAsync(string phoneNumber);
        public Task DeleteNotification(List<Notification> notifications);
        public Task<List<Notification>> GetNotificationByPhoneNumberAndDateRange(string phoneNumber, DateTime startDate, DateTime endDate);
        public Task UpdateNotification(Notification notification);
        public Task<Notification> GetNotificationByPhoneNumberAndTimestamp(string phoneNumber, string title, DateTime timestamp);
    }
}

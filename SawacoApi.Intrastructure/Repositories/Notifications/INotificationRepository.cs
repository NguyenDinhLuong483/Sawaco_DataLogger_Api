
namespace SawacoApi.Intrastructure.Repositories.Notifications
{
    public interface INotificationRepository
    {
        public Task AddNewNotificationAsync(Notification notification);
        public Task<bool> IsExistCustomer(string phoneNumber);
        public Task<List<Notification>> GetNotificationByPhoneNumberAsync(string phoneNumber);
    }
}

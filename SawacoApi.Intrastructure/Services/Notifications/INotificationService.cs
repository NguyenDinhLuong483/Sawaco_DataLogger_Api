
namespace SawacoApi.Intrastructure.Services.Notifications
{
    public interface INotificationService
    {
        public Task<bool> AddNewNotification(AddNewNotificationViewModel viewModel);
        public Task<List<NotificationViewModel>> GetNotificationByPhoneNumber(string phoneNumber);
    }
}


namespace SawacoApi.Intrastructure.Services.Notifications
{
    public interface INotificationService
    {
        public Task<bool> AddNewNotification(AddNewNotificationViewModel viewModel);
        public Task<List<NotificationViewModel>> GetNotificationByPhoneNumber(string phoneNumber);
        public Task<bool> DeleteNotification(string phoneNumber, DateTime startDate, DateTime endDate);
        public Task<List<NotificationViewModel>> GetNotificationByPhoneNumberAndDateRange(string phoneNumber, DateTime startDate, DateTime endDate);
        public Task<bool> UpdateNotification(UpdateNotificationViewModel viewModel);
        public Task<bool> AcknowledgeAll(string phoneNumber);
    }
}

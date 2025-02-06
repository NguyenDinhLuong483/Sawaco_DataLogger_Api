
namespace SawacoApi.Intrastructure.ViewModel.Notifications
{
    public class NotificationViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAcknowledge { get; set; }
    }
}

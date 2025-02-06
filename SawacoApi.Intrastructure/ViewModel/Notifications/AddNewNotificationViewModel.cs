
namespace SawacoApi.Intrastructure.ViewModel.Notifications
{
    public class AddNewNotificationViewModel
    {
        public string CustomerPhoneNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAcknowledge { get; set; }

        public AddNewNotificationViewModel(string customerPhoneNumber, string title, string description, DateTime timestamp, bool isAcknowledge)
        {
            CustomerPhoneNumber = customerPhoneNumber;
            Title = title;
            Description = description;
            Timestamp = timestamp;
            IsAcknowledge = isAcknowledge;
        }
    }
}

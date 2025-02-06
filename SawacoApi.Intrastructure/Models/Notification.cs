
namespace SawacoApi.Intrastructure.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string CustomerPhoneNumber {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAcknowledge { get; set; }
        public Customer Customer { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Notification()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public Notification(string id, string customerPhoneNumber, string title, string description, DateTime timestamp, bool isAcknowledge, Customer customer)
        {
            Id = id;
            CustomerPhoneNumber = customerPhoneNumber;
            Title = title;
            Description = description;
            Timestamp = timestamp;
            IsAcknowledge = isAcknowledge;
            Customer = customer;
        }
    }
}


namespace SawacoApi.Intrastructure.Models
{
    public class Customer
    {
        public string PhoneNumber { get; set; }
        public string UserName {  get; set; }
        public string Password {  get; set; }
        public List<GPSObject> GPSObjects { get; set; }
        public List<GPSDevice> GPSDevices { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Customer()
        {
        }

        public Customer(string phoneNumber, string userName, string password)
        {
            PhoneNumber = phoneNumber;
            UserName = userName;
            Password = password;
        }
    }
}

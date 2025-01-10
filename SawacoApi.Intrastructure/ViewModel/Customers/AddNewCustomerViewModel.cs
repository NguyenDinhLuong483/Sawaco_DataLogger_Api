
namespace SawacoApi.Intrastructure.ViewModel.Customers
{
    [DataContract]
    public class AddNewCustomerViewModel
    {
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }

        public AddNewCustomerViewModel(string phoneNumber, string userName, string password)
        {
            PhoneNumber = phoneNumber;
            UserName = userName;
            Password = password;
        }
    }
}

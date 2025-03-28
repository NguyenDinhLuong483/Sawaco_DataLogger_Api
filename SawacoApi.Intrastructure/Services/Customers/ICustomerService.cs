﻿
namespace SawacoApi.Intrastructure.Services.Customers
{
    public interface ICustomerService
    {
        public Task<bool> RegisterNewCustomer(AddNewCustomerViewModel customer);
        public Task<List<CustomerViewModel>> GetAllCustomers();
        public Task<CustomerViewModel> GetCustomerByPhoneNumber(string phoneNumber);
        public Task<bool> ChangeCustomerPhoneNumber(ChangeCustomerPhoneNumberViewModel viewmodel);
        public Task<bool> ChangeLoginInformation(ChangePasswordViewModel logininfo, string phoneNumber);
        public Task<bool> DeleteCustomerByPhoneNumber(string phoneNumber);
        public Task<string> Login(LoginViewModel loginViewModel);
    }
}

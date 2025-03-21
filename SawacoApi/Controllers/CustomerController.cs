﻿
using Microsoft.AspNetCore.Authorization;

namespace SawacoApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService _customerService {  get; set; }

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("RegisterNewCustomer")]
        public async Task<IActionResult> RegisterNewCustomer([FromBody] AddNewCustomerViewModel customer)
        {
            var isSuccess = await _customerService.RegisterNewCustomer(customer);
            if (isSuccess)
            {
                return new OkObjectResult("Registered successfully.");
            }
            else
            {
                return new OkObjectResult("Username already exists.");
            }
        }
        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<List<CustomerViewModel>> GetAllCustomers()
        {
            return await _customerService.GetAllCustomers();
        }
        [HttpGet]
        [Route("GetCustomerByPhoneNumber")]
        public async Task<CustomerViewModel> GetCustomerByPhoneNumber([FromQuery] string phoneNumber)
        {
            return await _customerService.GetCustomerByPhoneNumber(phoneNumber);
        }
        [HttpPatch]
        [Route("ChangeCustomerPhoneNumber")]
        public async Task<IActionResult> ChangeCustomerPhoneNumber([FromBody] ChangeCustomerPhoneNumberViewModel viewmodel)
        {
            var isSuccess = await _customerService.ChangeCustomerPhoneNumber(viewmodel);
            if (isSuccess)
            {
                return new OkObjectResult("Changed phone number successfully.");
            }
            else
            {
                return new OkObjectResult("Current phone number/password not correct or new phonenumber is exist!");
            }
        }
        [HttpPatch]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel passwordViewModel, [FromQuery] string phoneNumber)
        {
            var isSuccess = await _customerService.ChangeLoginInformation(passwordViewModel, phoneNumber);
            if (isSuccess)
            {
                return new OkObjectResult("Changed password successfully.");
            }
            else
            {
                return new OkObjectResult("Current phone number or password is not correct!");
            }
        }
        [HttpDelete]
        [Route("DeleteCustomerByPhoneNumber")]
        public async Task<IActionResult> DeleteCustomerByPhoneNumber([FromQuery] string customerPhoneNumber)
        {
            var isSuccess = await _customerService.DeleteCustomerByPhoneNumber(customerPhoneNumber);
            if (isSuccess)
            {
                return new OkObjectResult("Delete customer successfully.");
            }
            else
            {
                return new OkObjectResult("Phone number is not correct!");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var token = await _customerService.Login(login);
            return new OkObjectResult(token);
        }
    }
}


using Microsoft.IdentityModel.Tokens;
using SawacoApi.Intrastructure.ViewModel.Customers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SawacoApi.Intrastructure.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository _customerRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }
        private readonly JwtSetting _jwtSetting;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtSetting> jwtSetting)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSetting = jwtSetting.Value;
        }

        public async Task<bool> RegisterNewCustomer(AddNewCustomerViewModel customer)
        {
            var isexist = await _customerRepository.IsExistCustomer(customer.PhoneNumber);
            if (isexist)
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<AddNewCustomerViewModel, Customer>(customer);
                var userEntry = await _customerRepository.RegisterNewCustomer(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<CustomerViewModel>> GetAllCustomers()
        {
            var source = await _customerRepository.GetAllCustomerAsync();
            var resource = _mapper.Map<List<Customer>, List<CustomerViewModel>>(source);
            return resource;
        }

        public async Task<CustomerViewModel> GetCustomerByPhoneNumber(string phoneNumber)
        {
            var source = await _customerRepository.GetCustomerByPhoneNumberAsync(phoneNumber);
            var resource = _mapper.Map<Customer, CustomerViewModel>(source);
            return resource;
        }

        public async Task<bool> ChangeCustomerPhoneNumber(ChangeCustomerPhoneNumberViewModel viewmodel)
        {
            var isCorrect = await _customerRepository.IdentifyCustomer(viewmodel.CurrentPhoneNumber, viewmodel.Password);
            if (isCorrect)
            {
                var isExist = await _customerRepository.IsExistCustomer(viewmodel.NewPhoneNumber);
                if (isExist)
                {
                    return false;
                }
                else
                {
                    var customer = await _customerRepository.GetCustomerByPhoneNumberAsync(viewmodel.CurrentPhoneNumber);
                    
                    var newcustomerviewmodel = new AddNewCustomerViewModel(viewmodel.NewPhoneNumber, customer.UserName, customer.Password);
                    var newcustomer = _mapper.Map<AddNewCustomerViewModel, Customer>(newcustomerviewmodel);
                    await _customerRepository.RegisterNewCustomer(newcustomer);

                    var devices = await _customerRepository.GetGPSDeviceAsync(viewmodel.CurrentPhoneNumber);
                    if(devices is not null)
                    {
                        foreach(var device in devices)
                        {
                            device.CustomerPhoneNumber = viewmodel.NewPhoneNumber;
                        }
                        await _customerRepository.UpdateDevice(devices);
                    }
                    
                    var objects = await _customerRepository.GetGPSObjectAsync(viewmodel.CurrentPhoneNumber);
                    if (objects is not null)
                    {
                        foreach(var ob in objects)
                        {
                            ob.CustomerPhoneNumber = viewmodel.NewPhoneNumber;
                        }
                        await _customerRepository.UpdateObject(objects);
                    }

                    await _customerRepository.DeleteCustomer(customer);

                    return await _unitOfWork.CompleteAsync();
                }
            }
            else 
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomerByPhoneNumber(string phoneNumber)
        {
            var isExist = await _customerRepository.GetCustomerByPhoneNumberAsync(phoneNumber);
            if (isExist is not null)
            {
                await _customerRepository.DeleteCustomer(isExist);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeLoginInformation(ChangePasswordViewModel logininfo, string phoneNumber)
        {
            var isCorrect = await _customerRepository.IdentifyCustomer(phoneNumber, logininfo.CurrentPassword);
            if (isCorrect)
            {
                var customer = await _customerRepository.GetCustomerByPhoneNumberAsync(phoneNumber);
                customer.Password = logininfo.NewPassword;
                await _customerRepository.UpdateInformation(customer);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var customer = await _customerRepository.LoginAsync(loginViewModel);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myClaims = new[]
            {
                new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber),
                new Claim(ClaimTypes.Role, "Customer")
            };

            var token = new JwtSecurityToken(
                claims: myClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


namespace SawacoApi.Intrastructure.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository _customerRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                var customer = await _customerRepository.GetCustomerByPhoneNumberAsync(viewmodel.CurrentPhoneNumber);
                customer.PhoneNumber = viewmodel.NewPhoneNumber;
                await _customerRepository.UpdateInformation(customer);
                return await _unitOfWork.CompleteAsync();
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
    }
}

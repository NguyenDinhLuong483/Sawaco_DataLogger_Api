
namespace SawacoApi.Intrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        public INotificationRepository _notificationRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddNewNotification(AddNewNotificationViewModel viewModel)
        {
            var isExist = await _notificationRepository.IsExistCustomer(viewModel.CustomerPhoneNumber);
            if (isExist) 
            {
                var source = _mapper.Map<AddNewNotificationViewModel, Notification>(viewModel);
                await _notificationRepository.AddNewNotificationAsync(source);
                return await _unitOfWork.CompleteAsync();
            }
            else 
            {
                return false;
            }
        }

        public async Task<List<NotificationViewModel>> GetNotificationByPhoneNumber(string phoneNumber)
        {
            var source = await _notificationRepository.GetNotificationByPhoneNumberAsync(phoneNumber);
            var result = _mapper.Map<List<Notification>, List<NotificationViewModel>>(source);
            return result;
        }

        public async Task<bool> DeleteNotification(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            var source = await _notificationRepository.GetNotificationByPhoneNumberAndDateRange(phoneNumber, startDate, endDate);
            await _notificationRepository.DeleteNotification(source);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<NotificationViewModel>> GetNotificationByPhoneNumberAndDateRange(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            var source = await _notificationRepository.GetNotificationByPhoneNumberAndDateRange(phoneNumber, startDate, endDate);
            var result = _mapper.Map<List<Notification>, List<NotificationViewModel>>(source);
            return result;
        }

        public async Task<bool> UpdateNotification(UpdateNotificationViewModel viewModel)
        {
            var source = await _notificationRepository.GetNotificationByPhoneNumberAndTimestamp(viewModel.CustomerPhoneNumber, viewModel.Title, viewModel.Timestamp);
            source.IsAcknowledge = true;
            await _notificationRepository.UpdateNotification(source);
            return await _unitOfWork.CompleteAsync();
        }
    }
}

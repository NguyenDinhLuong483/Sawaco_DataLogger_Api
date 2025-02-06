
namespace SawacoApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public INotificationService _notificationService {  get; set; }

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("GetNotificationByPhoneNumber")]
        public async Task<List<NotificationViewModel>> GetNotificationByPhoneNumber(string phoneNumber)
        {
            return await _notificationService.GetNotificationByPhoneNumber(phoneNumber);                
        }

        [HttpPost]
        [Route("AddNewNotification")]
        public async Task<IActionResult> AddNewNotification(AddNewNotificationViewModel viewModel)
        {
            
            var isSuccess = await _notificationService.AddNewNotification(viewModel);
            if (isSuccess)
            {
                return new OkObjectResult("Add notification successfully.");
            }
            else
            {
                return new OkObjectResult("Phone Number is not correct!");
            }
            
        }
    }
}

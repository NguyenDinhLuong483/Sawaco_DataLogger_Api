
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
        //Query notification by phone number and date range
        [HttpGet]
        [Route("GetNotificationByPhoneNumberAndDateRange")]
        public async Task<List<NotificationViewModel>> GetNotificationByPhoneNumberAndDateRange(string phoneNumber, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _notificationService.GetNotificationByPhoneNumberAndDateRange(phoneNumber, startDate, endDate);
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
        [HttpDelete]
        [Route("DeleteNotification/{phoneNumber}")]
        public async Task<IActionResult> DeleteNotification(string phoneNumber, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var isSuccess = await _notificationService.DeleteNotification(phoneNumber, startDate, endDate);
            if (isSuccess)
            {
                return new OkObjectResult("Delete notification successfully.");
            }
            else
            {
                return new OkObjectResult("Delete notification failed!");
            }
        }
        [HttpPatch]
        [Route("UpdateNotification")]
        public async Task<IActionResult> UpdateNotification(UpdateNotificationViewModel viewModel)
        {
            var isSuccess = await _notificationService.UpdateNotification(viewModel);
            if (isSuccess)
            {
                return new OkObjectResult("Update notification successfully.");
            }
            else
            {
                return new OkObjectResult("Update notification failed!");
            }
        }
        [HttpPatch]
        [Route("AcknowledgeAll")]
        public async Task<IActionResult> AcknowledgeAll(string phoneNumber)
        {
            var isSuccess = await _notificationService.AcknowledgeAll(phoneNumber);
            if (isSuccess)
            {
                return new OkObjectResult("Acknowledge successfully.");
            }
            else
            {
                return new OkObjectResult("AcknowledgeAll failed!");
            }
        }
    }
}

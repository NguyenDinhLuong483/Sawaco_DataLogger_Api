
namespace SawacoApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        public IHistoryService _historyService { get; set; }

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        [Route("AddBatteryHistory")]
        public async Task<IActionResult> AddBatteryHistory(AddBatteryHistoryViewModel viewmodel)
        {
            var isSuccess = await _historyService.AddBatteryHistory(viewmodel);
            if (isSuccess)
            {
                return new OkObjectResult("Add battery history successfully.");
            }
            else
            {
                return new OkObjectResult("Error!");
            }
        }

        [HttpGet]
        [Route("GetBatteryHistory")]
        public async Task<List<BatteryHistoryViewModel>> GetBatteryHistory([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _historyService.GetBatteryHistory(startDate, endDate);
        }

        [HttpPost]
        [Route("AddDevicePositionHistory")]
        public async Task<IActionResult> AddDevicePositionHistory(AddDevicePositionHistoryViewModel viewmodel)
        {
            var isSuccess = await _historyService.AddDevicePositionHistory(viewmodel);
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
        [Route("GetDevicePositionHistory")]
        public async Task<List<DevicePositionHistoryViewModel>> GetDevicePositionHistory([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _historyService.GetDevicePositionHistory(startDate, endDate);
        }

        [HttpPost]
        [Route("AddDeviceObjectHistory")]
        public async Task<IActionResult> AddDeviceObjectHistory(AddObjectPositionHistoryViewModel viewmodel)
        {
            var isSuccess = await _historyService.AddObjectPositionHistory(viewmodel);
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
        [Route("GetDeviceObjectHistory")]
        public async Task<List<ObjectPositionHistoryViewModel>> GetDeviceObjectHistory([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _historyService.GetObjectPositionHistory(startDate, endDate);
        }
    }
}
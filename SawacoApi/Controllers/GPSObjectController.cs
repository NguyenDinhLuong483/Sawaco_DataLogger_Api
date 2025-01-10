
namespace SawacoApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GPSObjectController : ControllerBase
    {
        public IGPSObjectService _GPSObjectService { get; set; }

        public GPSObjectController(IGPSObjectService gPSObjectService)
        {
            _GPSObjectService = gPSObjectService;
        }

        [HttpGet]
        [Route("GetObjectById")]
        public async Task<GPSObjectViewModel> GetObjectById([FromQuery] string id)
        {
            return await _GPSObjectService.GetObjectById(id);
        }
        [HttpPost]
        [Route("CreateNewObject")]
        public async Task<IActionResult> CreateNewObject([FromBody] CreateNewObjectViewModel viewModel)
        {
            try
            {
                var result = await _GPSObjectService.CreateNewObject(viewModel);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch]
        [Route("SetupDeviceForObject")]
        public async Task<IActionResult> SetupDeviceForObject([FromBody] SetupDeviceViewModel viewModel)
        {
            var isSuccess = await _GPSObjectService.SetupDeviceForObject(viewModel);
            if (isSuccess)
            {
                return new OkObjectResult("Setup successfully!");
            }
            else
            {
                return new OkObjectResult("ObjectId or DeviceId is not correct!");
            }
        }
    }
}

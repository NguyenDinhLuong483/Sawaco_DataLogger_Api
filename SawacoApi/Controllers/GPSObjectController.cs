
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
        [Route("GetObjectByPhoneNumber")]
        public async Task<List<GPSObjectViewModel>> GetObjectByPhoneNumber([FromQuery] string phoneNumber) 
        {
            return await _GPSObjectService.GetObjectByPhoneNumber(phoneNumber);
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

        [HttpPatch]
        [Route("UpdateObjectInformation")]
        public async Task<IActionResult> UpdateObjectInformation([FromBody] UpdateGPSObjectViewModel viewModel, [FromQuery] string ObjectId)
        {
            var isSuccess = await _GPSObjectService.UpdateObjectInformation(viewModel, ObjectId);
            if (isSuccess)
            {
                return new OkObjectResult("Update successfully!");
            }
            else
            {
                return new OkObjectResult("Object Id is not correct!");
            }
        }

        [HttpPatch]
        [Route("CancelConnection")]
        public async Task<IActionResult> CancelConnection([FromQuery] string ObjectId)
        {
            var isSuccess = await _GPSObjectService.CancelConnection(ObjectId);
            if (isSuccess)
            {
                return new OkObjectResult("Cancle connection successfully!");
            }
            else
            {
                return new OkObjectResult("Object Id is not correct!");
            }
        }

        [HttpDelete]
        [Route("DeleteObjectById")]
        public async Task<IActionResult> DeleteObjectById([FromQuery] string objectId)
        {
            var isSuccess = await _GPSObjectService.DeleteObjectById(objectId);
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

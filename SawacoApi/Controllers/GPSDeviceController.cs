
namespace SawacoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GPSDeviceController : ControllerBase
    {
        public IGPSDeviceService _GPSDeviceService { get; set; }

        public GPSDeviceController(IGPSDeviceService gpsDeviceService)
        {
            _GPSDeviceService = gpsDeviceService;
        }

        [HttpGet]
        [Route("GetAllGPSDevices")]
        public async Task<List<GPSDeviceViewModel>> GetAllGPSDevices()
        {
            return await _GPSDeviceService.GetGPSDevice();
        }
        [HttpGet]
        [Route("GetGPSDeviceById")]
        public async Task<GPSDeviceViewModel> GetGPSDeviceById([FromQuery] string Id)
        {
            return await _GPSDeviceService.GetGPSDeviceById(Id);
        } 
        [HttpPost]
        [Route("CreateNewGPSDevice")]
        public async Task<IActionResult> CreateNewGPSDevice([FromBody] AddGPSDeviceViewModel addGPSDevice)
        {
            try
            {
                var result = await _GPSDeviceService.CreateNewGPSDevice(addGPSDevice);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("DeleteGPSDevice")]
        public async Task<IActionResult> DeleteGPSDevice([FromQuery] string GPSDeviceId) 
        {
            try
            {
                var result = await _GPSDeviceService.DeleteGPSDevice(GPSDeviceId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch]
        [Route("UpdateGPSDeviceStatus")]
        public async Task<IActionResult> UpdateGPSDeviceStatus([FromBody] UpdateGPSDeviceViewModel updateGPSDevice, [FromQuery] string GPSDeviceId)
        {
            try
            {
                var result = await _GPSDeviceService.UpdateGPSDeviceStatus(updateGPSDevice, GPSDeviceId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

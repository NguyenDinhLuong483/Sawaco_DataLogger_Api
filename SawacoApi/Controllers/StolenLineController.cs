
namespace SawacoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StolenLineController : ControllerBase
    {
        public IStolenLineService _stolenLineService { get; set; }

        public StolenLineController(IStolenLineService stolenLineService)
        {
            _stolenLineService = stolenLineService;
        }
        [HttpGet]
        [Route("GetStolenLineByDeviceId")]
        public async Task<List<StolenLineViewModel>> GetStolenLineByLoggerId([FromQuery] string DeviceId)
        {
            return await _stolenLineService.GetByDeviceId(DeviceId);
        }
        [HttpGet]
        [Route("GetStolenLineByDate")]
        public async Task<List<StolenLineViewModel>> GetStolenLineByDate([FromQuery] string id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _stolenLineService.GetByDate(id, startDate, endDate);
        }
        [HttpDelete]
        [Route("DeleteStolenLineByDeviceId")]
        public async Task<IActionResult> DeleteStolenLineByLoggerId([FromQuery] string DeviceId)
        {
            return new OkObjectResult(await _stolenLineService.DeleteByDeviceId(DeviceId));
        }
        [HttpDelete]
        [Route("DeleteStolenLineByDate")]
        public async Task<IActionResult> DeleteStolenLineByDate([FromQuery] string DeviceId, [FromQuery] DateTime startDate, [FromQuery]DateTime endDate)
        {
            return new OkObjectResult(await _stolenLineService.DeleteByDate(DeviceId, startDate, endDate));
        }
        [HttpPost]
        [Route("AddStolenLine")]
        public async Task<IActionResult> AddStolenLine([FromBody] AddStolenLineViewModel stolenLineViewModel)
        {
            return new OkObjectResult(await _stolenLineService.AddNewStolenLine(stolenLineViewModel));
        }

    }
}


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
        [Route("GetStolenLineByLoggerId")]
        public async Task<List<StolenLineViewModel>> GetStolenLineByLoggerId([FromQuery] string LoggerId)
        {
            return await _stolenLineService.GetByLoggerId(LoggerId);
        }
        [HttpGet]
        [Route("GetStolenLineByDate/Id={id}")]
        public async Task<List<StolenLineViewModel>> GetStolenLineByDate([FromRoute] string id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _stolenLineService.GetByDate(id, startDate, endDate);
        }
        [HttpDelete]
        [Route("DeleteStolenLineByLoggerId/GPSDeviceId={loggerId}")]
        public async Task<IActionResult> DeleteStolenLineByLoggerId([FromRoute] string loggerId)
        {
            return new OkObjectResult(await _stolenLineService.DeleteByLoggerId(loggerId));
        }
        [HttpDelete]
        [Route("DeleteStolenLineByDate/GPSDeviceId={loggerId}")]
        public async Task<IActionResult> DeleteStolenLineByDate([FromRoute] string loggerId, [FromQuery] DateTime startDate, [FromQuery]DateTime endDate)
        {
            return new OkObjectResult(await _stolenLineService.DeleteByDate(loggerId, startDate, endDate));
        }
        [HttpPost]
        [Route("AddStolenLine")]
        public async Task<IActionResult> AddStolenLine([FromBody] AddStolenLineViewModel stolenLineViewModel)
        {
            return new OkObjectResult(await _stolenLineService.AddNewStolenLine(stolenLineViewModel));
        }

    }
}

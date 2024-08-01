using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SawacoApi.Domain.Services;
using SawacoApi.Resources.StolenLine;

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
        [HttpDelete]
        [Route("DeleteStolenLineByLoggerId/LoggerId={LoggerId}")]
        public async Task<IActionResult> DeleteStolenLineByLoggerId([FromRoute] string LoggerId)
        {
            return new OkObjectResult(await _stolenLineService.DeleteByLoggerId(LoggerId));
        }
        [HttpPost]
        [Route("AddStolenLine")]
        public async Task<IActionResult> AddStolenLine([FromBody] AddStolenLineViewModel stolenLineViewModel)
        {
            return new OkObjectResult(await _stolenLineService.AddNewStolenLine(stolenLineViewModel));
        }
    }
}

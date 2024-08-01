using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SawacoApi.Domain.Services;
using SawacoApi.Resources;
using SawacoApi.Resources.Logger;

namespace SawacoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        public ILoggerService _loggerService { get; set; }

        public LoggerController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet]
        [Route("GetAllLoggers")]
        public async Task<List<LoggerViewModel>> GetAllLoggers()
        {
            return await _loggerService.GetLogger();
        }
        [HttpGet]
        [Route("GetLoggerById")]
        public async Task<LoggerViewModel> GetLoggerById([FromQuery] string Id)
        {
            return await _loggerService.GetLoggerById(Id);
        } 
        [HttpPost]
        [Route("CreateNewLogger")]
        public async Task<IActionResult> CreateNewLogger([FromBody] AddLoggerViewModel addLogger)
        {
            try
            {
                var result = await _loggerService.CreateNewLogger(addLogger);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("DeleteLogger/Id={LoggerId}")]
        public async Task<IActionResult> DeleteLogger([FromRoute] string LoggerId) 
        {
            try
            {
                var result = await _loggerService.DeleteLogger(LoggerId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch]
        [Route("UpdateLoggerStatus/Id={loggerId}")]
        public async Task<IActionResult> UpdateLoggerStatus([FromBody] UpdateLoggerViewModel updateLogger, [FromRoute] string loggerId)
        {
            try
            {
                var result = await _loggerService.UpdateLoggerStatus(updateLogger, loggerId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

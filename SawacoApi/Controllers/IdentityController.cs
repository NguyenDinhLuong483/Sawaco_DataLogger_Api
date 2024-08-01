using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SawacoApi.Domain.Services;
using SawacoApi.Resources.Identity;

namespace SawacoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public IIdentityService _identityService {  get; set; }

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewmodel)
        {
            return new OkObjectResult("true");
        }
    }
}

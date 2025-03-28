using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SawacoApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RouteOptimizeController : ControllerBase
    {
        private readonly IRouteOptimizeService _routeOptimizeService;
        public RouteOptimizeController(IRouteOptimizeService routeOptimizeService)
        {
            _routeOptimizeService = routeOptimizeService;
        }
        [HttpPost]
        public async Task<List<Coordinate>> OptimizeRoute([FromBody] List<Coordinate> coordinates)
        {
            var result = await _routeOptimizeService.FindShortestPath(coordinates);
            return result;
        }
    }
}


namespace SawacoApi.Intrastructure.Services.RouteOptimize
{
    public interface IRouteOptimizeService
    {
        public Task<List<Coordinate>> FindShortestPath(List<Coordinate> coordinateList);
    }
}

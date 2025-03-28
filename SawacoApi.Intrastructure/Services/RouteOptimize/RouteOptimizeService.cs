
namespace SawacoApi.Intrastructure.Services.RouteOptimize
{
    public class RouteOptimizeService : IRouteOptimizeService
    {
        private static double[,] distances;
        private static double[,] memo;
        private static int N;
        private static int END_STATE;

        public async Task<List<Coordinate>> FindShortestPath(List<Coordinate> coordinateList)
        {
            N = coordinateList.Count;
            distances = new double[N, N];
            memo = new double[N, 1 << N];
            END_STATE = (1 << N) - 1;

            // Calculate distances between each pair of points
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    distances[i, j] = GetDistance(coordinateList[i], coordinateList[j]);
                }
            }

            // Initialize memo table
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < (1 << N); j++)
                {
                    memo[i, j] = -1;
                }
            }

            // Start TSP from the first point
            await Task.Run(() => TSP(0, 1));

            // Reconstruct the path
            List<int> path = new List<int> { 0 };
            int lastIndex = 0;
            int state = 1;
            for (int i = 1; i < N; i++)
            {
                int bestIndex = -1;
                double bestDist = double.PositiveInfinity;

                for (int j = 0; j < N; j++)
                {
                    if ((state & (1 << j)) != 0) continue;
                    double newDist = distances[lastIndex, j] + memo[j, state | (1 << j)];
                    if (newDist < bestDist)
                    {
                        bestDist = newDist;
                        bestIndex = j;
                    }
                }

                path.Add(bestIndex);
                state |= (1 << bestIndex);
                lastIndex = bestIndex;
            }

            // Convert path to list of coordinates
            var resultPath = path.Select(index => coordinateList[index]).ToList();
            return resultPath;
        }

        private static double TSP(int i, int state)
        {
            if (state == END_STATE) return distances[i, 0];
            if (memo[i, state] != -1) return memo[i, state];

            double minCost = double.PositiveInfinity;
            for (int next = 0; next < N; next++)
            {
                if ((state & (1 << next)) != 0) continue;
                double newCost = distances[i, next] + TSP(next, state | (1 << next));
                minCost = Math.Min(minCost, newCost);
            }

            return memo[i, state] = minCost;
        }

        private static double GetDistance(Coordinate a, Coordinate b)
        {
            var d1 = a.Latitude * (Math.PI / 180.0);
            var num1 = a.Longitude * (Math.PI / 180.0);
            var d2 = b.Latitude * (Math.PI / 180.0);
            var num2 = b.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}

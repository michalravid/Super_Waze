using BL.Services.Preparing_infrastructure_for_the_store;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    //לקרוא את הקוד להבין אותו!!! ולסדר אותו שיתאים לצרכי המערכת שלנו ...
    public class ShopBL
    {
        private readonly ShopDTO _shop;
        public ShopBL(ShopDTO shop)
        {
            _shop = shop;
        }

        public bool[,] MapMatrix { get; set; }
        public int[,] DistanceMatrix { get; private set; }
        public Dictionary<(int, int), Dictionary<(int, int), (int, int)>> ParentMatrix { get; private set; }

        public Dictionary<(int, int), List<(int, int)>> BuildGraph()
        {
            var graph = new Dictionary<(int, int), List<(int, int)>>();

            int rows = _shop.MapMatrix.GetLength(0);
            int cols = _shop.MapMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (_shop.MapMatrix[i, j])
                    {
                        var neighbors = new List<(int, int)>();

                        if (i > 0 && _shop.MapMatrix[i - 1, j]) neighbors.Add((i - 1, j)); // Up
                        if (i < rows - 1 && _shop.MapMatrix[i + 1, j]) neighbors.Add((i + 1, j)); // Down
                        if (j > 0 && _shop.MapMatrix[i, j - 1]) neighbors.Add((i, j - 1)); // Left
                        if (j < cols - 1 && _shop.MapMatrix[i, j + 1]) neighbors.Add((i, j + 1)); // Right

                        graph[(i, j)] = neighbors;
                    }
                }
            }

            return graph;
        }

        public void BuildDistanceAndParentMatrices()
        {
            int rows = MapMatrix.GetLength(0);
            int cols = MapMatrix.GetLength(1);

            DistanceMatrix = new int[rows * cols, rows * cols];
            ParentMatrix = new Dictionary<(int, int), Dictionary<(int, int), (int, int)>>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!MapMatrix[i, j])
                        continue;

                    BFS((i, j));
                }
            }
        }

        private void BFS((int, int) start)
        {
            int rows = MapMatrix.GetLength(0);
            int cols = MapMatrix.GetLength(1);
            var queue = new Queue<(int, int)>();
            var visited = new bool[rows, cols];
            var parent = new Dictionary<(int, int), (int, int)>();

            queue.Enqueue(start);
            visited[start.Item1, start.Item2] = true;
            parent[start] = (-1, -1);

            int[] dirRow = { -1, 1, 0, 0 };
            int[] dirCol = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var currentIndex = current.Item1 * cols + current.Item2;

                for (int d = 0; d < 4; d++)
                {
                    int newRow = current.Item1 + dirRow[d];
                    int newCol = current.Item2 + dirCol[d];
                    var neighborIndex = newRow * cols + newCol;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                        !visited[newRow, newCol] && MapMatrix[newRow, newCol])
                    {
                        queue.Enqueue((newRow, newCol));
                        visited[newRow, newCol] = true;
                        parent[(newRow, newCol)] = current;

                        DistanceMatrix[currentIndex, neighborIndex] = DistanceMatrix[currentIndex, neighborIndex] + 1;
                    }
                }
            }
        }

        public List<(int, int)> FindPath((int, int) start, (int, int) end)
        {
            var path = new List<(int, int)>();
            var node = end;

            while (node != start)
            {
                path.Add(node);
                node = ParentMatrix[start][node];
            }

            path.Add(start);
            path.Reverse();

            return path;
        }

        public List<(int, int)> FindFullPath(int start, int end, int[] points)
        {
            // מציאת רשימת הצמתים החשובים בסדר נכון
            List<(int, int)> criticalPoints = FindShortestPath(start, end, points);

            // מסלול מלא שמכיל את כל הנקודות במסלול בין כל זוג צמתים
            var fullPath = new List<(int, int)>();

            for (int i = 0; i < criticalPoints.Count - 1; i++)
            {
                var segmentPath = FindPath(criticalPoints[i], criticalPoints[i + 1]);
                fullPath.AddRange(segmentPath);
            }

            return fullPath;
        }

        private void BFS(Dictionary<(int, int), List<(int, int)>> graph, (int, int) startNode)
        {
            int rows = _shop.MapMatrix.GetLength(0);
            int cols = _shop.MapMatrix.GetLength(1);
            var queue = new Queue<((int, int), int)>();
            var visited = new HashSet<(int, int)>();

            queue.Enqueue((startNode, 0));
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                var (currentNode, distance) = queue.Dequeue();

                foreach (var neighbor in graph[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue((neighbor, distance + 1));

                        int startIdx = startNode.Item1 * cols + startNode.Item2;
                        int neighborIdx = neighbor.Item1 * cols + neighbor.Item2;
                        DistanceMatrix[startIdx, neighborIdx] = distance + 1;
                        DistanceMatrix[neighborIdx, startIdx] = distance + 1;
                    }
                }
            }
        }
        public List<(int, int)> FindShortestPath(int start, int end, int[] points)
        {
            int n = points.Length + 2; // כולל נקודת התחלה ונקודת סיום
            int[,] dist = new int[n, n];

            dist[0, 0] = 0; // start to start
            dist[n - 1, n - 1] = 0; // end to end

            // שלב 1: בניית מטריצת המרחקים
            for (int i = 0; i < points.Length; i++)
            {
                dist[0, i + 1] = DistanceMatrix[start, points[i]];
                dist[i + 1, 0] = DistanceMatrix[start, points[i]];

                dist[n - 1, i + 1] = DistanceMatrix[end, points[i]];
                dist[i + 1, n - 1] = DistanceMatrix[end, points[i]];

                for (int j = i + 1; j < points.Length; j++)
                {
                    dist[i + 1, j + 1] = DistanceMatrix[points[i], points[j]];
                    dist[j + 1, i + 1] = DistanceMatrix[points[i], points[j]];
                }
            }

            // שלב 2: חישוב המסלול הקצר ביותר (Held-Karp)
            var dp = new int[1 << n, n];
            var parent = new int[1 << n, n]; // נשתמש במערך זה כדי לשחזר את המסלול

            for (int i = 0; i < (1 << n); i++)
                for (int j = 0; j < n; j++)
                {
                    dp[i, j] = int.MaxValue / 2;
                    parent[i, j] = -1;
                }

            dp[1 << 0, 0] = 0;

            for (int mask = 0; mask < (1 << n); mask++)
            {
                for (int u = 0; u < n; u++)
                {
                    if ((mask & (1 << u)) == 0) continue;

                    for (int v = 0; v < n; v++)
                    {
                        if ((mask & (1 << v)) != 0) continue;

                        int newDist = dp[mask, u] + dist[u, v];
                        if (newDist < dp[mask | (1 << v), v])
                        {
                            dp[mask | (1 << v), v] = newDist;
                            parent[mask | (1 << v), v] = u;
                        }
                    }
                }
            }

            int minLength = int.MaxValue;
            int lastNode = -1;
            for (int u = 1; u < n - 1; u++)
            {
                int currentLength = dp[(1 << n) - 1, u] + dist[u, n - 1];
                if (currentLength < minLength)
                {
                    minLength = currentLength;
                    lastNode = u;
                }
            }

            // שלב 3: שיחזור המסלול
            var path = new List<(int, int)>();
            int currentNode = lastNode;
            int currentMask = (1 << n) - 1;

            while (currentNode != -1)
            {
                if (currentNode == 0)
                {
                    path.Add(IndexToPoint(start));
                }
                else if (currentNode == n - 1)
                {
                    path.Add(IndexToPoint(end));
                }
                else
                {
                    path.Add(IndexToPoint(points[currentNode - 1]));
                }

                int prevNode = parent[currentMask, currentNode];
                currentMask ^= (1 << currentNode);
                currentNode = prevNode;
            }

            path.Reverse(); // כיוון שאנחנו בונים את המסלול מהסוף להתחלה, נהפוך אותו

            return path;
        }

        private (int, int) IndexToPoint(int index)
        {
            int rows = MapMatrix.GetLength(0);
            int cols = MapMatrix.GetLength(1);
            return (index / cols, index % cols);
        }
    }
}
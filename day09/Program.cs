var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray())
    .ToArray();

var map = new HeatMap(input);
System.Console.WriteLine($"Part 1: Sum of the risk level is {map.SumOfRiskLevel}");
System.Console.WriteLine($"Part 2: Product of the three largest basins is {map.ProductOfThreeLargestBasins}");

public class HeatMap
{
    int[,] map;
    public HeatMap(char[][] mapInput)
    {
        var sizeX = mapInput[0].Length;
        var sizeY = mapInput.Length;
        map = new int[sizeX, sizeY];

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                map[x, y] = Convert.ToInt32(mapInput[y][x].ToString());
            }
        }

    }
    public int SumOfRiskLevel
    {
        get
        {
            var lowPoints = LowPoints();
            return lowPoints.Sum(p => map[p.X, p.Y] + 1);
        }
    }
    public int ProductOfThreeLargestBasins
    {
        get
        {
            List<int> basinSizes = new List<int>();
            var lowPoints = LowPoints();
            foreach (var point in lowPoints)
            {
                basinSizes.Add(BasinSize(point, new HashSet<Point>()));
            }
            return basinSizes.OrderByDescending(s => s).Take(3).Aggregate(1, (x, y) => x * y);
        }
    }
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point Up => new Point { X = X, Y = Y - 1 };
        public Point Down => new Point { X = X, Y = Y + 1 };
        public Point Right => new Point { X = X + 1, Y = Y };
        public Point Left => new Point { X = X - 1, Y = Y };
    }
    IEnumerable<Point> LowPoints()
    {
        var sizeX = map.GetLength(0);
        var sizeY = map.GetLength(1);
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                var value = map[x, y];
                //List<int> surroundingPoints = new List<int>();
                //int tl = (y == 0 || x == 0) ? 0 : map[x - 1, y - 1];
                int to = (y == 0) ? 10 : map[x, y - 1];
                //int tr = (y == 0 || x == map.GetLength(0) ? 0 : map[x + 1, y]);
                int le = (x == 0) ? 10 : map[x - 1, y];
                int ri = (x == sizeX - 1) ? 10 : map[x + 1, y];
                //int ll = (y == map.GetLength(1) || x == 0) ? 0 : map[x - 1, y + 1];
                int lo = (y == sizeY - 1) ? 10 : map[x, y + 1];
                //int lr = (x == map.GetLength(0) || y == map.GetLength(1)) ? 0 : map[x + 1, y + 1];

                if (value < to && value < le && value < ri && value < lo)
                {
                    yield return new Point { X = x, Y = y };
                }
            }
        }
    }
    int BasinSize(Point point, HashSet<Point> visitedPoints)
    {
        if (map[point.X, point.Y] == 9) return 0;
        visitedPoints.Add(point);
        var visitedPointsCount = 1;
        if (OnMap(point.Up) && !visitedPoints.Contains(point.Up)) visitedPointsCount += BasinSize(point.Up, visitedPoints);
        if (OnMap(point.Right) && !visitedPoints.Contains(point.Right)) visitedPointsCount += BasinSize(point.Right, visitedPoints);
        if (OnMap(point.Down) && !visitedPoints.Contains(point.Down)) visitedPointsCount += BasinSize(point.Down, visitedPoints);
        if (OnMap(point.Left) && !visitedPoints.Contains(point.Left)) visitedPointsCount += BasinSize(point.Left, visitedPoints);
        return visitedPointsCount;
    }
    bool OnMap(Point point) => (point.X >= 0 && point.X < map.GetLength(0) && point.Y >= 0 && point.Y < map.GetLength(1));
}
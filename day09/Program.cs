var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray())
    .ToArray();

var map = new HeatMap(input);
System.Console.WriteLine(map.SumOfRiskLevel);


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
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
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
}
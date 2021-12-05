using System.Text.RegularExpressions;

var regex = new Regex(@"([0-9]+,[0-9]+) -> ([0-9]+,[0-9]+)");

var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => regex.Match(l));

Part1(1000, 1000, lines);

System.Console.WriteLine();

Part2(1000, 1000, lines);

void Part1(int sizeX, int sizeY, IEnumerable<Match> lines)
{
    var map = new Map(sizeX, sizeY);

    foreach (var line in lines)
    {
        map.AddStraightLine(line);
    }

    System.Console.WriteLine($"Part 1: {map.Overlaps} overlapping points");
}

void Part2(int sizeX, int sizeY, IEnumerable<Match> lines)
{
    var map = new Map(sizeX, sizeY);

    foreach (var line in lines)
    {
        map.AddLine(line);
    }

    System.Console.WriteLine($"Part 2: {map.Overlaps} overlapping points");
}

internal class Map
{
    int[,] hydrothermalVentMap;

    public Map(int x, int y)
    {
        hydrothermalVentMap = new int[x, y];
    }

    public void AddStraightLine(Match line)
    {
        var p1 = new Point { X = int.Parse(line.Groups[1].Value.Split(',')[0]), Y = int.Parse(line.Groups[1].Value.Split(',')[1]) };
        var p2 = new Point { X = int.Parse(line.Groups[2].Value.Split(',')[0]), Y = int.Parse(line.Groups[2].Value.Split(',')[1]) };

        // Straight lines only
        if (p1.X == p2.X || p1.Y == p2.Y)
        {
            Fill(p1, p2);
        }
    }
    public void AddLine(Match line)
    {
        var p1 = new Point { X = int.Parse(line.Groups[1].Value.Split(',')[0]), Y = int.Parse(line.Groups[1].Value.Split(',')[1]) };
        var p2 = new Point { X = int.Parse(line.Groups[2].Value.Split(',')[0]), Y = int.Parse(line.Groups[2].Value.Split(',')[1]) };
        Fill(p1, p2);
    }

    void Fill(Point p1, Point p2)
    {
        var incX = Math.Sign(p2.X - p1.X);
        var incY = Math.Sign(p2.Y - p1.Y);

        int x = p1.X;
        int y = p1.Y;

        while (x != p2.X || y != p2.Y)
        {
            hydrothermalVentMap[x, y]++;
            x += incX;
            y += incY;
        }
        hydrothermalVentMap[x, y]++;
    }

    // For visualization only
    public void Print()
    {
        for (int y = 0; y < hydrothermalVentMap.GetLength(1); y++)
        {
            for (int x = 0; x < hydrothermalVentMap.GetLength(0); x++)
            {
                if (hydrothermalVentMap[x, y] > 0)
                    Console.Write(hydrothermalVentMap[x, y]);
                else
                    Console.Write('.');
            }
            System.Console.WriteLine();
        }
    }

    public int Overlaps => hydrothermalVentMap.Cast<int>().Where(n => n > 1).Count();

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

}
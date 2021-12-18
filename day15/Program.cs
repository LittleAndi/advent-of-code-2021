var lines = File.ReadAllLines(args[0]).Where(l => !string.IsNullOrWhiteSpace(l));
int size = lines.Count();
int[,] map = new int[size, size];

for (int y = 0; y < size; y++)
{
    var line = lines.ElementAt(y).ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
    for (int x = 0; x < size; x++)
    {
        map[x, y] = line[x];
    }
}

long totalPaths = 0;
var totalRisk = Traverse(0, 0);

System.Console.WriteLine($"Minimum risk {totalRisk}, {totalPaths} tested");

// (99,99): 298155000000

int Traverse(int x, int y)
{
    if ((x == size - 1) && (y == size - 1))
    {
        if (++totalPaths % 1000000 == 0)
            System.Console.WriteLine($"({x},{y}): {totalPaths}");
        return map[x, y];
    }

    int risk = (x > 0 || y > 0) ? map[x, y] : 0;

    // try right
    int riskX = int.MaxValue;
    if (x < size - 1) riskX = risk + Traverse(x + 1, y);

    // try down
    int riskY = int.MaxValue;
    if (y < size - 1) riskY = risk + Traverse(x, y + 1);

    risk = (riskX < riskY) ? riskX : riskY;
    // System.Console.WriteLine($"({x},{y}): {risk}");
    return risk;
}
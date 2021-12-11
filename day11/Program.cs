// See https://aka.ms/new-console-template for more information
using System.Text;

var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray().Select(c => Convert.ToInt32(c.ToString())));

var cave = new Cave(input);
cave.RunSteps(100);
System.Console.WriteLine($"Part 1: {cave.FlashCount} total flashes after 100 steps.");

// Reset cave
cave = new Cave(input);
var steps = 0;
var flashes = 0;
do
{
    steps++;
    flashes = cave.Step();
} while (flashes != 100);

System.Console.WriteLine($"Part 2: All octopuses flashes at step {steps}.");

public class Cave
{
    Dictionary<Octopus, int> octopusEnergies = new Dictionary<Octopus, int>();
    int totalFlashCount = 0;
    public Cave(IEnumerable<IEnumerable<int>> octopuses)
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                this.octopusEnergies.Add(new Octopus { X = x, Y = y }, octopuses.ElementAt(y).ElementAt(x));
            }
        }
    }

    public int BottomLeft => octopusEnergies.First(o => o.Key.X == 0 && o.Key.Y == 9).Value;
    public int FlashCount => totalFlashCount;

    public int Step()
    {
        int flashesOnThisStep = 0;
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                var thisOctopus = new Octopus { X = x, Y = y };
                octopusEnergies[thisOctopus]++;
            }
        }
        flashesOnThisStep += CheckFlashes();
        totalFlashCount += flashesOnThisStep;
        return flashesOnThisStep;
    }
    public void RunSteps(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            Step();
        }
    }

    private int CheckFlashes()
    {
        int loops = 0;
        int flashes = 0;
        var flashedOctopuses = new HashSet<Octopus>();
        foreach (var octopusEnergy in octopusEnergies.Where(o => o.Value > 9))
        {
            loops++;
            flashes++;
            octopusEnergies[octopusEnergy.Key] = 0;
            flashedOctopuses.Add(octopusEnergy.Key);
            flashes += FlashSurroundings(octopusEnergy.Key, flashedOctopuses);
        }
        return flashes;
    }
    private int FlashSurroundings(Octopus octopus, HashSet<Octopus> flashedOctopuses)
    {
        int flashes = 0;
        if (octopus.N.InCave() && !flashedOctopuses.Contains(octopus.N) && ++octopusEnergies[octopus.N] > 9 && flashedOctopuses.Add(octopus.N)) { flashes++; octopusEnergies[octopus.N] = 0; flashes += FlashSurroundings(octopus.N, flashedOctopuses); }
        if (octopus.E.InCave() && !flashedOctopuses.Contains(octopus.E) && ++octopusEnergies[octopus.E] > 9 && flashedOctopuses.Add(octopus.E)) { flashes++; octopusEnergies[octopus.E] = 0; flashes += FlashSurroundings(octopus.E, flashedOctopuses); }
        if (octopus.S.InCave() && !flashedOctopuses.Contains(octopus.S) && ++octopusEnergies[octopus.S] > 9 && flashedOctopuses.Add(octopus.S)) { flashes++; octopusEnergies[octopus.S] = 0; flashes += FlashSurroundings(octopus.S, flashedOctopuses); }
        if (octopus.W.InCave() && !flashedOctopuses.Contains(octopus.W) && ++octopusEnergies[octopus.W] > 9 && flashedOctopuses.Add(octopus.W)) { flashes++; octopusEnergies[octopus.W] = 0; flashes += FlashSurroundings(octopus.W, flashedOctopuses); }
        if (octopus.NE.InCave() && !flashedOctopuses.Contains(octopus.NE) && ++octopusEnergies[octopus.NE] > 9 && flashedOctopuses.Add(octopus.NE)) { flashes++; octopusEnergies[octopus.NE] = 0; flashes += FlashSurroundings(octopus.NE, flashedOctopuses); }
        if (octopus.SE.InCave() && !flashedOctopuses.Contains(octopus.SE) && ++octopusEnergies[octopus.SE] > 9 && flashedOctopuses.Add(octopus.SE)) { flashes++; octopusEnergies[octopus.SE] = 0; flashes += FlashSurroundings(octopus.SE, flashedOctopuses); }
        if (octopus.SW.InCave() && !flashedOctopuses.Contains(octopus.SW) && ++octopusEnergies[octopus.SW] > 9 && flashedOctopuses.Add(octopus.SW)) { flashes++; octopusEnergies[octopus.SW] = 0; flashes += FlashSurroundings(octopus.SW, flashedOctopuses); }
        if (octopus.NW.InCave() && !flashedOctopuses.Contains(octopus.NW) && ++octopusEnergies[octopus.NW] > 9 && flashedOctopuses.Add(octopus.NW)) { flashes++; octopusEnergies[octopus.NW] = 0; flashes += FlashSurroundings(octopus.NW, flashedOctopuses); }
        return flashes;
    }
    public void Print()
    {
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                var o = new Octopus { X = x, Y = y };
                sb.Append(octopusEnergies[o]);
            }
            sb.Append("\n");
        }
        System.Console.WriteLine(sb.ToString());
    }
}
public struct Octopus
{
    public int X { get; set; }
    public int Y { get; set; }
    public Octopus N => new Octopus { X = X, Y = Y - 1 };
    public Octopus NE => new Octopus { X = X + 1, Y = Y - 1 };
    public Octopus E => new Octopus { X = X + 1, Y = Y };
    public Octopus SE => new Octopus { X = X + 1, Y = Y + 1 };
    public Octopus S => new Octopus { X = X, Y = Y + 1 };
    public Octopus SW => new Octopus { X = X - 1, Y = Y + 1 };
    public Octopus W => new Octopus { X = X - 1, Y = Y };
    public Octopus NW => new Octopus { X = X - 1, Y = Y - 1 };
}

static class Extensions
{
    public static bool InCave(this Octopus p) => p.X >= 0 && p.X < 10 && p.Y >= 0 && p.Y < 10;
}

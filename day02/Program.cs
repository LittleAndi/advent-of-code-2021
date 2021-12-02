var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => new CommandLine(l.Split(' ')[0], int.Parse(l.Split(' ')[1])))
    .ToList<CommandLine>();

var submarine = new Submarine();

foreach (var commandLine in lines)
{
    switch (commandLine.command)
    {
        case "forward": submarine.Forward(commandLine.distance); break;
        case "up": submarine.Up(commandLine.distance); break;
        case "down": submarine.Down(commandLine.distance); break;
    }
}

System.Console.WriteLine($"Part 1: Multiplied position is {submarine.MultipliedPosition}");

var newImprovedSubmarine = new Submarine();

foreach (var commandLine in lines)
{
    switch (commandLine.command)
    {
        case "forward": newImprovedSubmarine.ForwardWithAim(commandLine.distance); break;
        case "up": newImprovedSubmarine.AimUp(commandLine.distance); break;
        case "down": newImprovedSubmarine.AimDown(commandLine.distance); break;
    }
}

System.Console.WriteLine($"Part 1: Multiplied position is {newImprovedSubmarine.MultipliedPosition}");

internal record CommandLine(string command, int distance);
internal class Submarine
{
    int horizontal = 0;
    int depth = 0;
    int aim = 0;

    public void Forward(int distance) { horizontal += distance; }
    public void Down(int distance) { depth += distance; }
    public void Up(int distance) { depth -= distance; }
    public void AimDown(int x) { aim += x; }
    public void AimUp(int x) { aim -= x; }
    public void ForwardWithAim(int distance) { horizontal += distance; depth += distance * aim; }
    public int MultipliedPosition => horizontal * depth;
}
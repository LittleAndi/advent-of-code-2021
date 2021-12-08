var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.Split(" | "))
    .ToArray();

var output = lines.Select(l => l[1].Split(' '));

int count = 0;
foreach (var item in output)
{
    foreach (var j in item)
    {
        if (j.Length == 2 || j.Length == 3 || j.Length == 4 || j.Length == 7) count++;
    }
}

System.Console.WriteLine(count);

var displays = lines.Select(l => new Display(l[0].Split(' '), l[1].Split(' ')));

public class Display
{
    string[] patterns;
    string[] output;
    public Display(string[] patterns, string[] output)
    {
        this.patterns = patterns;
        this.output = output;
    }


}

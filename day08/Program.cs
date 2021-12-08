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
    Dictionary<char, HashSet<char>> segments;
    public Display(string[] patterns, string[] output)
    {
        this.patterns = patterns;
        this.output = output;
        this.segments = new Dictionary<char, HashSet<char>>
        {
            { 'a', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
            { 'b', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
            { 'c', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
            { 'd', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
            { 'e', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
            { 'f', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
            { 'g', new HashSet<char>{ 'a','b','c','d','e','f','g' } },
        };
        Reduce();


    }
    private void Reduce()
    {

    }

    public string Zero => $"{segments['a'].First()}{segments['b'].First()}{segments['c'].First()}{{segments['e'].First()}}{{segments['f'].First()}}{{segments['g'].First()}}" + segments['b'] + segments['c'] + segments['e'] + segments['f'] + segments['g'];
    public string One => $"{segments['c'].First()}{segments['f'].First()}";
}

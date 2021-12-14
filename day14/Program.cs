var lines = File.ReadAllLines("input.txt");

var start = lines[0].ToCharArray().ToList();
var rules = new Dictionary<string, char>(lines.Skip(2).Select(l => new KeyValuePair<string, char>(l.Split(" -> ")[0], l.Split(" -> ")[1][0])));

var p = new PolymerizationEquipment(start, rules);

for (int i = 0; i < 10; i++)
{
    p.ApplyRules();
}

System.Console.WriteLine(p.MostCommonElementCount - p.LeastCommonElementCount);

for (int i = 0; i < 30; i++)
{
    p.ApplyRules();
    System.Console.WriteLine($"{i}:\t{p.MostCommonElementCount - p.LeastCommonElementCount}");
}

System.Console.WriteLine(p.MostCommonElementCount - p.LeastCommonElementCount);


public class PolymerizationEquipment
{
    List<char> polymer;
    Dictionary<string, char> rules;

    public PolymerizationEquipment(List<char> polymerTemplate, Dictionary<string, char> pairInsertionRules)
    {
        this.polymer = polymerTemplate;
        this.rules = pairInsertionRules;
    }
    public string Polymer => string.Join("", this.polymer);
    public void ApplyRules()
    {
        var newList = new List<char>();
        for (int i = 0; i <= polymer.Count - 2; i++)
        {
            var pair = new string(new char[] { polymer[i], polymer[i + 1] });
            newList.Add(polymer[i]);
            newList.Add(rules[pair]);
        }
        newList.Add(polymer.Last());

        this.polymer = newList;
    }

    public long MostCommonElementCount
    {
        get
        {
            var t = this.polymer.Cast<char>().ToList(); //.GroupBy(p => p);
            var g = t.GroupBy(c => c).Select(group => new KeyValuePair<char, long>(group.Key, group.Count()));
            return g.OrderByDescending(c => c.Value).First().Value;
        }
    }
    public long LeastCommonElementCount
    {
        get
        {
            var t = this.polymer.Cast<char>().ToList(); //.GroupBy(p => p);
            var g = t.GroupBy(c => c).Select(group => new KeyValuePair<char, long>(group.Key, group.Count()));
            return g.OrderBy(c => c.Value).First().Value;
        }
    }
}
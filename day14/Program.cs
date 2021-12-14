var lines = File.ReadAllLines("input.txt");

var start = new LinkedList<char>();
lines[0].ToCharArray().ToList().ForEach(c => start.AddLast(c));
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
    LinkedList<char> polymer;
    Dictionary<string, char> rules;

    public PolymerizationEquipment(LinkedList<char> polymerTemplate, Dictionary<string, char> pairInsertionRules)
    {
        this.polymer = polymerTemplate;
        this.rules = pairInsertionRules;
    }

    public string ApplyRules()
    {
        var c = polymer.First;
        var newList = new LinkedList<char>();

        do
        {
            var pair = new string(new char[] { c.Value, c.Next.Value });

            newList.AddLast(c.Value);
            newList.AddLast(rules[pair]);
            c = c.Next;
        } while (c.Next != null);

        newList.AddLast(c.Value);

        this.polymer = newList;

        return string.Join("", newList.ToArray());
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
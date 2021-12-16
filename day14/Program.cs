var lines = File.ReadAllLines("input.txt");

var start = lines[0].ToCharArray().ToList();
var rules = new Dictionary<string, char>(lines.Skip(2).Select(l => new KeyValuePair<string, char>(l.Split(" -> ")[0], l.Split(" -> ")[1][0])));

var p = new PolymerizationEquipment(start, rules);

for (int i = 0; i < 10; i++)
{
    p.ApplyRules();
}

System.Console.WriteLine($"Part 1: The difference between most common and least common element is {p.MostCommonElementCount - p.LeastCommonElementCount}");

for (int i = 0; i < 30; i++)
{
    p.ApplyRules();
}

System.Console.WriteLine($"Part 2: The difference between most common and least common element is {p.MostCommonElementCount - p.LeastCommonElementCount}");

public class PolymerizationEquipment
{
    List<char> polymerTemplate;
    Dictionary<string, long> polymerPairs = new Dictionary<string, long>();
    char endingChar;
    Dictionary<string, char> rules;

    public PolymerizationEquipment(List<char> polymerTemplate, Dictionary<string, char> pairInsertionRules)
    {
        for (int i = 0; i <= polymerTemplate.Count - 2; i++)
        {
            var pair = new string(new char[] { polymerTemplate[i], polymerTemplate[i + 1] });
            if (!polymerPairs.TryAdd(pair, 1)) polymerPairs[pair]++;
        }
        endingChar = polymerTemplate.Last();

        this.polymerTemplate = polymerTemplate;
        this.rules = pairInsertionRules;
    }
    public void ApplyRules()
    {
        var newPolymerPairs = new Dictionary<string, long>();
        foreach (var pair in polymerPairs)
        {
            var newPair = new string(new char[2] { pair.Key[0], rules[pair.Key] });
            if (!newPolymerPairs.TryAdd(newPair, pair.Value)) newPolymerPairs[newPair] += pair.Value;
            newPair = new string(new char[2] { rules[pair.Key], pair.Key[1] });
            if (!newPolymerPairs.TryAdd(newPair, pair.Value)) newPolymerPairs[newPair] += pair.Value;
        }

        this.polymerPairs = newPolymerPairs;
    }
    public int PolymerPairs => this.polymerPairs.Count;
    public long MostCommonElementCount
    {
        get
        {
            Dictionary<char, long> chars = new Dictionary<char, long>();

            foreach (var pair in polymerPairs)
            {
                if (!chars.TryAdd(pair.Key[0], pair.Value)) chars[pair.Key[0]] += pair.Value;
            }
            var lastChar = polymerTemplate.Last();
            if (!chars.TryAdd(lastChar, polymerPairs.Last().Value)) chars[lastChar]++;
            return chars.OrderByDescending(c => c.Value).First().Value;
        }
    }
    public long LeastCommonElementCount
    {
        get
        {
            Dictionary<char, long> chars = new Dictionary<char, long>();

            foreach (var pair in polymerPairs)
            {
                if (!chars.TryAdd(pair.Key[0], pair.Value)) chars[pair.Key[0]] += pair.Value;
            }
            var lastChar = polymerTemplate.Last();
            if (!chars.TryAdd(lastChar, polymerPairs.Last().Value)) chars[lastChar]++;
            return chars.OrderBy(c => c.Value).First().Value;
        }
    }
}
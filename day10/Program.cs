var chunks = File.ReadAllLines("input_test.txt")
    .Select(l => !string.IsNullOrWhiteSpace(l));

public class NavigationSystem
{
    IEnumerable<string> chunks;
    Dictionary<char, int> score = new Dictionary<char, int>
    {
        {')', 3 },
        {']', 57 },
        {'}', 1197 },
        {'>', 25137 },
    };

    public NavigationSystem(IEnumerable<string> chunks)
    {
        this.chunks = chunks;
    }
    public int TotalSyntaxErrorScore
    {
        get
        {
            var totalSyntaxErrorScore = 0;
            foreach (var chunk in chunks)
            {
                var firstIllegalCharacter = FirstIllegalCharacter(chunk);
                if (firstIllegalCharacter != null)
                {
                    totalSyntaxErrorScore += score[(char)firstIllegalCharacter];
                }
            }
            return totalSyntaxErrorScore;
        }
    }
    char? FirstIllegalCharacter(string chunk)
    {
        Dictionary<char, int> closed = new Dictionary<char, int>()
        {
            {')', 0 },
            {']', 0 },
            {'}', 0 },
            {'>', 0 },
        };
        foreach (var c in chunk.ToCharArray())
        {
            if (!closed.ContainsKey(c))
            {
                if (c == '(') closed[')']++;
                if (c == '[') closed[']']++;
                if (c == '{') closed['}']++;
                if (c == '<') closed['>']++;
            }
            else
            {
                if (closed.Where(v => v.Key != c && v.Value > 0).Any()) return c;
                if (--closed[c] < 0) return c;
                closed[c]--;
            }
        }
        return null;
    }
}
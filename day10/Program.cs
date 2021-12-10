var chunks = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l));

var nav = new NavigationSystem(chunks);
System.Console.WriteLine(nav.TotalSyntaxErrorScore);

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
        var stack = new Stack<char>();
        foreach (var c in chunk.ToCharArray())
        {
            if (c == '(') { stack.Push(')'); continue; }
            if (c == '[') { stack.Push(']'); continue; }
            if (c == '{') { stack.Push('}'); continue; }
            if (c == '<') { stack.Push('>'); continue; }

            var popped = stack.Pop();
            if (popped != c) return c;
        }
        return null;
    }
}
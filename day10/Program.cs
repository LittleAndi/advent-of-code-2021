var chunks = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l));

var nav = new NavigationSystem(chunks);
System.Console.WriteLine($"Part 1: Total syntax error score is {nav.TotalSyntaxErrorScore}");
System.Console.WriteLine($"Part 2: Incomplete lines middle score is {nav.IncompleteLinesMiddleScore}");

public class NavigationSystem
{
    IEnumerable<string> chunks;
    Dictionary<char, int> errorScoreMap = new Dictionary<char, int>
    {
        {')', 3 },
        {']', 57 },
        {'}', 1197 },
        {'>', 25137 },
    };
    Dictionary<char, int> completionScoreMap = new Dictionary<char, int>
    {
        {')', 1 },
        {']', 2 },
        {'}', 3 },
        {'>', 4 },
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
                (var firstIllegalCharacter, _) = FirstIllegalCharacterAndRemainingStack(chunk);
                if (firstIllegalCharacter != null)
                    totalSyntaxErrorScore += errorScoreMap[(char)firstIllegalCharacter];
            }
            return totalSyntaxErrorScore;
        }
    }
    public long IncompleteLinesMiddleScore
    {
        get
        {
            List<long> completionScores = new List<long>();
            foreach (var chunk in chunks)
            {
                (var illegalCharacter, var remainingStack) = FirstIllegalCharacterAndRemainingStack(chunk);
                if (illegalCharacter == null)
                {
                    long completionScore = 0;
                    while (remainingStack.Count > 0)
                    {
                        completionScore = completionScore * 5 + completionScoreMap[remainingStack.Pop()];
                    }
                    completionScores.Add(completionScore);
                }
            }
            return completionScores.OrderBy(s => s).ElementAt(completionScores.Count / 2);
        }
    }
    (char? illegalCharacter, Stack<char> remainingStack) FirstIllegalCharacterAndRemainingStack(string chunk)
    {
        var stack = new Stack<char>();
        foreach (var c in chunk.ToCharArray())
        {
            switch (c)
            {
                case '(': stack.Push(')'); break;
                case '[': stack.Push(']'); break;
                case '{': stack.Push('}'); break;
                case '<': stack.Push('>'); break;
                default:
                    var popped = stack.Pop();
                    if (popped != c) return (c, stack);
                    break;
            }
        }
        return (null, stack);
    }
}
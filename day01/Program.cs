var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => int.Parse(l))
    .ToArray<int>();

int increaseCount = 0;
for (int i = 1; i < lines.Length; i++)
{
    if (lines[i - 1] < lines[i]) increaseCount++;
}

System.Console.WriteLine($"Part 1: {increaseCount}");

increaseCount = 0;
for (int i = 3; i < lines.Length; i++)
{
    // a + b + c < b + c + d => a < d
    if (lines[i - 3] < lines[i]) increaseCount++;
}

System.Console.WriteLine($"Part 2: {increaseCount}");

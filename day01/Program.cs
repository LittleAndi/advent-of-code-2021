var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => int.Parse(l))
    .ToArray<int>();

int increase = 0;
int previous = 10000;
foreach (var item in lines)
{
    if (item > previous) increase++;
    previous = item;
}

System.Console.WriteLine(increase);

increase = 0;
previous = 10000;
for (int i = 2; i < lines.Length; i++)
{
    var sum = lines[i - 2] + lines[i - 1] + lines[i];
    if (sum > previous) increase++;
    previous = sum;
}

System.Console.WriteLine(increase);
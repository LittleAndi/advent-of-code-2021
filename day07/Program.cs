var input = File.ReadAllLines("input.txt")[0].Split(',').Select(n => int.Parse(n)).ToArray();

var mean = input.OrderBy(n => n).ToArray()[input.Length / 2];
System.Console.WriteLine($"Mean: {mean}");

var fuel = 0;
for (int i = 0; i < input.Length; i++)
{
    fuel += Math.Abs(input[i] - mean);
}

System.Console.WriteLine($"Part 1: {fuel}");

var avg = (int)Math.Round(input.Average(), 0) - 1;
System.Console.WriteLine($"Avg: {avg}");

fuel = 0;
for (int i = 0; i < input.Length; i++)
{
    for (int j = 1; j <= Math.Abs(input[i] - avg); j++)
    {
        fuel += j;
    }
}

System.Console.WriteLine($"Part 2: {fuel}");

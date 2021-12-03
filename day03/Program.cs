var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray())
    .ToArray();

var s = "";

for (int i = 0; i < lines[0].Length; i++)
{
    var x = lines.OrderBy(l => l[i]).ElementAt(lines.Length / 2)[i];
    s += x;
}
var gamma_rate = Convert.ToInt32(s, 2);
System.Console.WriteLine($"gamma rate: {gamma_rate}");

var t = "";
for (int i = 0; i < s.Length; i++)
{
    if (s[i] == '0')
    {
        t += "1";

    }
    else
    {
        t += "0";
    }
}
var epsilon_rate = Convert.ToInt32(t, 2);
System.Console.WriteLine($"epsilone rate: {epsilon_rate}");

System.Console.WriteLine($"Part 1: {gamma_rate * epsilon_rate}");

// part 2
var oxygen_generator_rating = 0;
{
    var process = lines;
    for (int i = 0; i < lines[0].Length; i++)
    {
        var zeros = process.Where(l => l[i] == '0').Count();
        var ones = process.Where(l => l[i] == '1').Count();
        if (ones > zeros || ones == zeros)
        {
            process = process.Where(p => p[i] == '1').ToArray();
        }
        else
        {
            process = process.Where(p => p[i] == '0').ToArray();
        }
    }
    System.Console.WriteLine(process[0]);
    oxygen_generator_rating = Convert.ToInt32(new string(process[0]), 2);
    System.Console.WriteLine(oxygen_generator_rating);
}
var CO2_scrubber_rating = 0;
{
    var process = lines;
    for (int i = 0; i < lines[0].Length; i++)
    {
        var zeros = process.Where(l => l[i] == '0').Count();
        var ones = process.Where(l => l[i] == '1').Count();
        if (ones > zeros || ones == zeros)
        {
            process = process.Where(p => p[i] == '0').ToArray();
        }
        else
        {
            process = process.Where(p => p[i] == '1').ToArray();
        }
        if (process.Length == 1) break;
    }
    System.Console.WriteLine(process[0]);
    CO2_scrubber_rating = Convert.ToInt32(new string(process[0]), 2);
    System.Console.WriteLine(CO2_scrubber_rating);
}

System.Console.WriteLine($"Part 2: {oxygen_generator_rating * CO2_scrubber_rating}");
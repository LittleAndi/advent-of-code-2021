var input = File.ReadAllLines("input.txt")[0].Split(',').Select(n => int.Parse(n)).ToList();

System.Console.WriteLine($"Part 1: {Simulate(input, 80)}");
System.Console.WriteLine($"Part 2: {Simulate(input, 256)}");

long Simulate(List<int> input, int days)
{
    long[] laternFishesAge = new long[9];
    foreach (var item in input)
    {
        laternFishesAge[item]++;
    }
    for (int day = 0; day < days; day++)
    {
        long saveZeroes = laternFishesAge[0];

        for (int age = 0; age < 8; age++)
        {
            laternFishesAge[age] = laternFishesAge[age + 1];
        }

        laternFishesAge[6] += saveZeroes;
        laternFishesAge[8] = saveZeroes;
    }
    return laternFishesAge.Sum();
}

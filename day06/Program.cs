var lines = File.ReadAllLines("input.txt")[0].Split(',').Select(n => int.Parse(n)).ToList();

//var lines = new List<int> { 3, 4, 3, 1, 2 };

for (int day = 0; day < 80; day++)
{
    var laternFisheCount = lines.Count;
    for (int i = 0; i < laternFisheCount; i++)
    {
        if (lines[i] == 0)
        {
            lines[i] = 6;
            lines.Add(8);
        }
        else
        {
            lines[i]--;
        }
    }
}

System.Console.WriteLine(lines.Count);
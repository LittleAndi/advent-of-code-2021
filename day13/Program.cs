var lines = File.ReadAllLines("input.txt");

var coords = lines.Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("fold")).Select(l => l.Split(',').Select(c => int.Parse(c)).ToArray());
var instructions = lines
                    .Where(l => l.StartsWith("fold"))
                    .Select(l => new KeyValuePair<char, int>(l.Replace("fold along ", "").Split('=')[0][0], int.Parse(l.Replace("fold along ", "").Split('=')[1])))
                    .ToList();

var sheet = coords.ToSheet();

for (int i = 0; i < instructions.Count; i++)
{
    var instruction = instructions[i];
    switch (instruction.Key)
    {
        case 'x': sheet = sheet.FoldX<char?>(instruction.Value); break;
        case 'y': sheet = sheet.FoldY<char?>(instruction.Value); break;
    }
    if (i == 0) System.Console.WriteLine($"Part 1: {sheet.Cast<char?>().Count(c => c == '#')} dots after first fold.");
}
System.Console.WriteLine("Part 2: The code is");
sheet.Print<char?>();

public static class Extensions
{
    public static char?[,] ToSheet(this IEnumerable<int[]> coords)
    {
        int maxX = coords.Max(c => c[0]) + 1;
        int maxY = coords.Max(c => c[1]) + 1;
        var sheet = new char?[maxX, maxY];
        foreach (var item in coords)
        {
            sheet[item[0], item[1]] = '#';
        }
        return sheet;
    }
    public static T[,] FoldX<T>(this T[,] array, int x)
    {
        var leftSheet = array.SliceVertical<T>(0, x);
        var rightSheet = array.SliceVertical<T>(x + 1, array.GetLength(0));
        var result = leftSheet.FlipMergeVertical(rightSheet);
        return result;
    }
    public static T[,] FoldY<T>(this T[,] array, int y)
    {
        var topSheet = array.SliceHorizontal<T>(0, y);
        var bottomSheet = array.SliceHorizontal<T>(y + 1, array.GetLength(1));
        var result = topSheet.FlipMergeHorizontal<T>(bottomSheet);
        return result;
    }
    public static T[,] SliceHorizontal<T>(this T[,] array, int from, int to)
    {
        T[,] slice = new T[array.GetLength(0), to - from];

        for (int y = from; y < to; y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                slice[x, y - from] = array[x, y];
            }
        }

        return slice;
    }

    public static T[,] SliceVertical<T>(this T[,] array, int from, int to)
    {
        T[,] slice = new T[to - from, array.GetLength(1)];

        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = from; x < to; x++)
            {
                slice[x - from, y] = array[x, y];
            }
        }

        return slice;
    }

    public static T[,] FlipMergeHorizontal<T>(this T[,] array, T[,] arrayToMerge)
    {
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                array[x, y] = arrayToMerge[x, arrayToMerge.GetLength(1) - 1 - y] ?? array[x, y];
            }
        }

        return array;
    }

    public static T[,] FlipMergeVertical<T>(this T[,] array, T[,] arrayToMerge)
    {
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                array[x, y] = arrayToMerge[arrayToMerge.GetLength(0) - 1 - x, y] ?? array[x, y];
            }
        }

        return array;
    }

    public static void Print<T>(this T[,] array)
    {
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                Console.Write(array[x, y] as char? ?? '.');
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine();
    }

}
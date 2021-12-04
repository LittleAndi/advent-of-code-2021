var lines = File.ReadAllLines("input.txt")
    .ToArray();

int[] drawNumbers = lines[0].Split(',').Select(n => int.Parse(n)).ToArray();

System.Console.WriteLine($"Numbers to draw: {drawNumbers.Length}");

// Create games
var games = new List<BingoGame>();
for (int i = 2; i < lines.Length - 2; i += 6)
{
    var boardNumbers = To2D(lines[i..(i + 5)].Select(l => l.Split(" ", 5, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray()).ToArray());
    games.Add(new BingoGame(boardNumbers));
}

// Draw numbers and check for wins
for (int i = 0; i < drawNumbers.Length; i++)
{
    foreach (var game in games)
    {
        if (game.FullRowOrColumn) continue;
        if (game.Draw(drawNumbers[i]))
        {
            // Win
            Console.WriteLine($"When {drawNumbers[i]} was drawn, board with index {games.IndexOf(game)} won with score {game.SumOfUnmarkedNumbers * drawNumbers[i]}");
        }
    }
}

int[,] To2D(int[][] source)
{
    var result = new int[5, 5];
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            result[i, j] = source[i][j];
        }
    }
    return result;
}

public class BingoGame
{
    int[,] _boardNumbers = new int[5, 5];
    public BingoGame(int[,] boardNumbers)
    {
        _boardNumbers = boardNumbers;
    }

    public bool Draw(int number)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (_boardNumbers[i, j] == number)
                {
                    _boardNumbers[i, j] = -1;
                }
            }
        }
        return FullRowOrColumn;
    }

    public bool FullRowOrColumn
    {
        get
        {
            for (int i = 0; i < 5; i++)
            {
                if (GetRow(_boardNumbers, i).Sum() == -5) return true;
                if (GetColumn(_boardNumbers, i).Sum() == -5) return true;
            }
            return false;
        }

    }
    public int SumOfUnmarkedNumbers
    {
        get
        {
            return _boardNumbers.Cast<int>().Where(n => n >= 0).Sum();
        }
    }
    IEnumerable<int> GetRow(int[,] array, int row)
    {
        for (int i = 0; i <= array.GetUpperBound(1); ++i)
            yield return array[row, i];
    }

    IEnumerable<int> GetColumn(int[,] array, int column)
    {
        for (int i = 0; i <= array.GetUpperBound(0); ++i)
            yield return array[i, column];
    }
}


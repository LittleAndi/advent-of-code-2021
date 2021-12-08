var lines = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.Split(" | "))
    .ToArray();

var output = lines.Select(l => l[1].Split(' '));

int count = 0;
foreach (var item in output)
{
    foreach (var j in item)
    {
        if (j.Length == 2 || j.Length == 3 || j.Length == 4 || j.Length == 7) count++;
    }
}

System.Console.WriteLine($"Part 1: {count}");

var displays = lines.Select(l => new Display(l[0].Split(' ').ToList(), l[1].Split(' ')));

System.Console.WriteLine($"part 2: {displays.Sum(d => d.Output)}");

public class Display
{
    /*
     8 : 7 segments
     0 : 6 segments
     6 : 6 segments
     9 : 6 segments
     2 : 5 segments
     3 : 5 segments
     5 : 5 segments
     4 : 4 segments
     7 : 3 segments
     1 : 2 segments
    */

    List<string> patterns;
    string[] output;
    Dictionary<char, char> segments;
    Dictionary<string, char> digits;
    public Display(List<string> patterns, string[] output)
    {
        this.patterns = patterns.OrderBy(p => p.Length).ToList();
        this.output = output;
        this.segments = new Dictionary<char, char>();
        this.digits = new Dictionary<string, char>();
        Reduce();
    }
    private void Reduce()
    {
        char[]? zero = null;
        char[]? one = patterns.Where(p => p.Length == 2).First().ToCharArray();
        char[]? two = null;
        char[]? three = null;
        char[]? four = patterns.Where(p => p.Length == 4).First().ToCharArray();
        char[]? five = null;
        char[]? six = null;
        char[]? seven = patterns.Where(p => p.Length == 3).First().ToCharArray();
        char[]? eight = patterns.Where(p => p.Length == 7).First().ToCharArray();
        char[]? nine = null;

        patterns.Remove(new string(one));
        patterns.Remove(new string(four));
        patterns.Remove(new string(seven));
        patterns.Remove(new string(eight));

        // If we remove ONE from SEVEN, we get top segment (a)
        segments.Add('a', seven.Except(one).First());

        // If we remove FOUR and SEVEN from NINE, we get the bottom segment (g)
        // (But we have to find the combination that results in one char left)
        // We have 3 possible numbers with 6 segments
        {
            var possibleNines = patterns.Where(p => p.Length == 6);
            foreach (var possibleNine in possibleNines)
            {
                var result = possibleNine.ToCharArray().Except(four).Except(seven);
                if (result.Count() == 1)
                {
                    segments.Add('g', result.First());
                    nine = possibleNine.ToCharArray();
                    patterns.Remove(possibleNine);
                    break;
                }
            }
        }

        // If we remove SEVEN and bottom segment (g) from THREE, we get the middle segment (d)
        {
            var possibleThrees = patterns.Where(p => p.Length == 5);
            foreach (var possibleThree in possibleThrees)
            {
                var result = possibleThree.ToCharArray().Except(seven).Except(new char[] { segments['g'] });
                if (result.Count() == 1)
                {
                    segments.Add('d', result.First());
                    three = possibleThree.ToCharArray();
                    patterns.Remove(possibleThree);
                    break;
                }
            }
        }

        // If we remove ONE, FOUR and segments a, d, g from TWO, we get the bottom left segment (e)
        // Should be two possible numbers with 5 segments left
        {
            var possibleTwos = patterns.Where(p => p.Length == 5);
            foreach (var possibleTwo in possibleTwos)
            {
                var result = possibleTwo.ToCharArray().Except(one).Except(four).Except(new char[] { segments['a'], segments['d'], segments['g'] });
                if (result.Count() == 1)
                {
                    segments.Add('e', result.First());
                    two = possibleTwo.ToCharArray();
                    patterns.Remove(possibleTwo);
                    break;
                }
            }
        }

        // If we now remove THREE from FIVE, we get the upper left segment (b)
        {
            var possibleFives = patterns.Where(p => p.Length == 5);
            foreach (var possibleFive in possibleFives)
            {
                var result = possibleFive.ToCharArray().Except(three);
                if (result.Count() == 1)
                {
                    segments.Add('b', result.First());
                    five = possibleFive.ToCharArray();
                    patterns.Remove(possibleFive);
                    break;
                }
            }
        }

        // The common segment between ONE and TWO is the top right segment (c)
        {
            segments.Add('c', two.Intersect(one).First());
        }

        // The last segment (f) should be the one where we remove segment (c) from ONE
        {
            segments.Add('f', one.Except(new char[] { segments['c'] }).First());
        }

        six = eight.Except(new char[] { segments['c'] }).ToArray();
        zero = eight.Except(new char[] { segments['d'] }).ToArray();

        // Build digits for output mapping
        digits.Add(new string(zero.OrderBy(c => c).ToArray()), '0');
        digits.Add(new string(one.OrderBy(c => c).ToArray()), '1');
        digits.Add(new string(two.OrderBy(c => c).ToArray()), '2');
        digits.Add(new string(three.OrderBy(c => c).ToArray()), '3');
        digits.Add(new string(four.OrderBy(c => c).ToArray()), '4');
        digits.Add(new string(five.OrderBy(c => c).ToArray()), '5');
        digits.Add(new string(six.OrderBy(c => c).ToArray()), '6');
        digits.Add(new string(seven.OrderBy(c => c).ToArray()), '7');
        digits.Add(new string(eight.OrderBy(c => c).ToArray()), '8');
        digits.Add(new string(nine.OrderBy(c => c).ToArray()), '9');
    }
    private char Digit(string output)
    {
        var sortedOutput = new string(output.ToCharArray().OrderBy(c => c).ToArray());
        return digits[sortedOutput];
    }

    public int Output => int.Parse(
        new string(new char[] {
            Digit(this.output[0]),
            Digit(this.output[1]),
            Digit(this.output[2]),
            Digit(this.output[3]),
        }));
}

using System.Linq;
using Xunit;
using Shouldly;

namespace day08tests;

public class TestDisplayOutputs
{
    [Theory]
    [InlineData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", "cdfeb fcadb cdfeb cdbaf", 5353)]
    [InlineData("cebdg ebaf bca cdeba ecabdf ab dgbacf fcaedg gdabefc cadfe", "gdbacf bac fgdaebc eafcd", 785)]
    public void Test1(string patternString, string outputString, int expectedOutput)
    {
        var patterns = patternString.Split(' ').ToList();
        var output = outputString.Split(' ').ToArray();
        var d = new Display(patterns, output);
        d.Output.ShouldBe(expectedOutput);
    }
}
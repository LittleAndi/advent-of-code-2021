using System.Linq;
using Xunit;
using Shouldly;

namespace day08tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var patterns = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab".Split(' ').ToArray();
        var output = "cdfeb fcadb cdfeb cdbaf".Split(' ').ToArray();
        var d = new Display(patterns, output);
        d.One.ShouldBe("ab");
    }
}
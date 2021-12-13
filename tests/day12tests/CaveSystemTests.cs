using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace day12tests;

public class CaveSystemTests
{
    [Theory]
    [InlineData("input_test_1.txt")]
    [InlineData("input_test_2.txt")]
    [InlineData("input_test_3.txt")]
    [InlineData("input_test_4.txt")]
    public void TestInput(string filename)
    {
        var input = File.ReadAllLines(filename)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        var caveSystem = new CaveSystem(input);
    }

    [Theory]
    [InlineData("input_test_1.txt", 10)]
    [InlineData("input_test_2.txt", 19)]
    [InlineData("input_test_3.txt", 226)]
    [InlineData("input_test_4.txt", 3)]
    public void TestPossiblePaths(string filename, int possiblePaths)
    {
        var input = File.ReadAllLines(filename)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        var caveSystem = new CaveSystem(input);
        caveSystem.PossiblePathsWithMaxOneVisitToSmallCave.ShouldBe(possiblePaths);
    }

    [Theory]
    [InlineData("input_test_1.txt", 36)]
    [InlineData("input_test_2.txt", 103)]
    [InlineData("input_test_3.txt", 3509)]
    public void TestPossiblePathsWithMaxTwoVisitsToSmallCaves(string filename, int possiblePaths)
    {
        var input = File.ReadAllLines(filename)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        var caveSystem = new CaveSystem(input);
        caveSystem.PossiblePathsWithMaxTwoVisitsToSmallCaves.ShouldBe(possiblePaths);
    }
}
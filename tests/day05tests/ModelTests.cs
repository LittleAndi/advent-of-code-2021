using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Shouldly;
using Xunit;

namespace day05tests;

public class ModelTests
{
    [Fact]
    public void TestMapWithStraightLinesOnly()
    {
        var regex = new Regex(@"([0-9]+,[0-9]+) -> ([0-9]+,[0-9]+)");
        var lines = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => regex.Match(l));
        var map = new Map(10, 10);
        map.Overlaps.ShouldBe(0);

        foreach (var line in lines)
        {
            map.AddStraightLine(line);
        }

        map.Overlaps.ShouldBe(5);
    }
    [Fact]
    public void TestMapWithAllLines()
    {
        var regex = new Regex(@"([0-9]+,[0-9]+) -> ([0-9]+,[0-9]+)");
        var lines = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => regex.Match(l));
        var map = new Map(10, 10);
        map.Overlaps.ShouldBe(0);

        foreach (var line in lines)
        {
            map.AddLine(line);
        }

        map.Overlaps.ShouldBe(12);
    }
}
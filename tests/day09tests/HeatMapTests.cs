using Shouldly;
using Xunit;

namespace day09tests;

public class HeatMapTests
{
    [Fact]
    public void Test1()
    {
        var input = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray())
            .ToArray();

        var map = new HeatMap(input);

        map.SumOfRiskLevel.ShouldBe(15);
    }
}
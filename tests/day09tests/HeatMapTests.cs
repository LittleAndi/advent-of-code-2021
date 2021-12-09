using Shouldly;
using Xunit;

namespace day09tests;

public class HeatMapTests
{
    HeatMap map;
    public HeatMapTests()
    {
        var input = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray())
            .ToArray();

        map = new HeatMap(input);
    }
    [Fact]
    public void TestSumOfRiskLevel()
    {
        map.SumOfRiskLevel.ShouldBe(15);
    }

    [Fact]
    public void TestProductOfThreeLargestBasins()
    {
        map.ProductOfThreeLargestBasins.ShouldBe(1134);
    }
}
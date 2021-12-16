using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace day14tests;

public class PolymerizationEquipmentTests
{
    [Fact]
    public void TestApplyRules()
    {
        var lines = File.ReadAllLines("input_test.txt");

        var start = lines[0].ToCharArray().ToList();
        var rules = new Dictionary<string, char>(lines.Skip(2).Select(l => new KeyValuePair<string, char>(l.Split(" -> ")[0], l.Split(" -> ")[1][0])));

        var p = new PolymerizationEquipment(start, rules);
        p.MostCommonElementCount.ShouldBe(2);
        p.LeastCommonElementCount.ShouldBe(1);
        p.ApplyRules();
        p.MostCommonElementCount.ShouldBe(2);
        p.LeastCommonElementCount.ShouldBe(1);
        p.ApplyRules();
        p.MostCommonElementCount.ShouldBe(6);
        p.LeastCommonElementCount.ShouldBe(1);
        p.ApplyRules();
        p.MostCommonElementCount.ShouldBe(11);
        p.LeastCommonElementCount.ShouldBe(4);
        p.ApplyRules();
        p.MostCommonElementCount.ShouldBe(23);
        p.LeastCommonElementCount.ShouldBe(5);
    }

    [Fact]
    public void MostCommonElementCount()
    {
        var lines = File.ReadAllLines("input_test.txt");

        var start = lines[0].ToCharArray().ToList();
        var rules = new Dictionary<string, char>(lines.Skip(2).Select(l => new KeyValuePair<string, char>(l.Split(" -> ")[0], l.Split(" -> ")[1][0])));

        var p = new PolymerizationEquipment(start, rules);
        for (int i = 0; i < 10; i++)
        {
            p.ApplyRules();
        }
        p.MostCommonElementCount.ShouldBe(1749);
        p.LeastCommonElementCount.ShouldBe(161);
    }

}
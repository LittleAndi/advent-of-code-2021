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
        p.ApplyRules();
        p.Polymer.ShouldBe("NCNBCHB");
        p.ApplyRules();
        p.Polymer.ShouldBe("NBCCNBBBCBHCB");
        p.ApplyRules();
        p.Polymer.ShouldBe("NBBBCNCCNBBNBNBBCHBHHBCHB");
        p.ApplyRules();
        p.Polymer.ShouldBe("NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB");
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
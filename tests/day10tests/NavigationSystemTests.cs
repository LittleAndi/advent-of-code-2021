using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace day10tests;

public class NavigationSystemTests
{
    NavigationSystem nav;
    public NavigationSystemTests()
    {
        var chunks = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l));
        nav = new NavigationSystem(chunks);
    }
    [Fact]
    public void TestTotalSyntaxErrorScore()
    {
        nav.TotalSyntaxErrorScore.ShouldBe(26397);
    }
    [Theory]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", 1197)]
    [InlineData("[[<[([]))<([[{}[[()]]]", 3)]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", 57)]
    [InlineData("[<(<(<(<{}))><([]([]()", 3)]
    [InlineData("<{([([[(<>()){}]>(<<{{", 25137)]
    [InlineData("<(<[[<{(<[(<[([]())]({<>()}({}()))>[({(){}})[[[][]][<>{}]]]){{[[<><>]{{}[]}]<[(){}]<{}()>>}(<(<>{", 0)]
    public void TestSyntaxErrorScore(string chunk, int error)
    {
        var nav = new NavigationSystem(new List<string> { chunk });
        nav.TotalSyntaxErrorScore.ShouldBe(error);
    }
    [Fact]
    public void TestIncompleteLinesMiddleScore()
    {
        nav.IncompleteLinesMiddleScore.ShouldBe(288957);
    }
}
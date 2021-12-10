using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace day10tests;

public class NavigationSystemTests
{
    [Fact]
    public void TestTotalSyntaxErrorScore()
    {
        var chunks = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l));
        var nav = new NavigationSystem(chunks);
        nav.TotalSyntaxErrorScore.ShouldBe(26397);
    }
    [Theory]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", 1197)]
    [InlineData("[[<[([]))<([[{}[[()]]]", 3)]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", 57)]
    [InlineData("[<(<(<(<{}))><([]([]()", 3)]
    [InlineData("<{([([[(<>()){}]>(<<{{", 25137)]
    public void TestSyntaxErrorScore(string chunk, int error)
    {
        var nav = new NavigationSystem(new List<string> { chunk });
        nav.TotalSyntaxErrorScore.ShouldBe(error);
    }
}
using System;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace day11tests;

public class CaveTests
{
    Cave cave;
    public CaveTests()
    {
        var input = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray().Select(c => Convert.ToInt32(c.ToString())));

        cave = new Cave(input);
    }
    [Fact]
    public void TestInput()
    {
        cave.BottomLeft.ShouldBe(5);
    }

    [Fact]
    public void TestFlashesAfterOneHundredSteps()
    {
        cave.RunSteps(100);
        cave.FlashCount.ShouldBe(1656);
    }

    [Fact]
    public void TestName()
    {
        // This one needs its own cave
        var input = File.ReadAllLines("input_test.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray().Select(c => Convert.ToInt32(c.ToString())));

        var cave = new Cave(input);
        cave.Step().ShouldBe(0);
        cave.Print();
        cave.Step().ShouldBe(35);
        cave.Print();
        cave.Step().ShouldBe(45);
        cave.Print();
        cave.Step().ShouldBe(16);
        cave.Print();
    }
}
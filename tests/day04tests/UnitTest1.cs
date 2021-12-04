using Shouldly;
using Xunit;

namespace day04tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var boardNumbers = new int[,]
        {
            {14,21,17,24, 4},
            {10,16,15, 9,19},
            {18, 8,23,26,20},
            {22,11,13, 6, 5},
            { 2, 0,12, 3, 7},
        };
        var board = new BingoGame(boardNumbers);
        board.Draw(7).ShouldBeFalse();
        board.Draw(4).ShouldBeFalse();
        board.Draw(9).ShouldBeFalse();
        board.Draw(5).ShouldBeFalse();
        board.Draw(11).ShouldBeFalse();
        board.Draw(17).ShouldBeFalse();
        board.Draw(23).ShouldBeFalse();
        board.Draw(2).ShouldBeFalse();
        board.Draw(0).ShouldBeFalse();
        board.Draw(14).ShouldBeFalse();
        board.Draw(21).ShouldBeFalse();
        board.Draw(24).ShouldBeTrue();

        board.SumOfUnmarkedNumbers.ShouldBe(188);
    }
}
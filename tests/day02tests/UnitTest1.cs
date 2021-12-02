using Shouldly;
using Xunit;

namespace day02tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var s = new Submarine();
        s.Forward(5);
        s.Down(5);
        s.Forward(8);
        s.Up(3);
        s.Down(8);
        s.Forward(2);
        s.MultipliedPosition.ShouldBe(150);
    }

    [Fact]
    public void Test2()
    {
        var s = new Submarine();
        s.ForwardWithAim(5);
        s.AimDown(5);
        s.ForwardWithAim(8);
        s.AimUp(3);
        s.AimDown(8);
        s.ForwardWithAim(2);
        s.MultipliedPosition.ShouldBe(900);
    }
}
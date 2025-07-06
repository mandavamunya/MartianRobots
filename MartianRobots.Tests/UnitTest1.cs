using Xunit;
using MartianRobots;
using System.Collections.Generic;

namespace MartianRobots.Tests;

public class RobotTests
{
    [Fact]
    public void RobotMovesForwardCorrectly()
    {
        var robot = new Robot(new Position(1, 1, Orientation.N));
        var scent = new HashSet<(int, int, Orientation)>();
        robot.Execute("F", 5, 5, scent);
        Assert.Equal("1 2 N", robot.GetOutput());
    }

    [Fact]
    public void RobotTurnsLeftAndRightCorrectly()
    {
        var robot = new Robot(new Position(1, 1, Orientation.N));
        var scent = new HashSet<(int, int, Orientation)>();
        robot.Execute("R", 5, 5, scent);
        Assert.Equal("1 1 E", robot.GetOutput());
        robot.Execute("L", 5, 5, scent);
        Assert.Equal("1 1 N", robot.GetOutput());
    }

    [Fact]
    public void RobotGetsLostAndLeavesScent()
    {
        var robot = new Robot(new Position(0, 0, Orientation.S));
        var scent = new HashSet<(int, int, Orientation)>();
        robot.Execute("F", 5, 5, scent);
        Assert.Equal("0 0 S LOST", robot.GetOutput());
        Assert.Contains((0, 0, Orientation.S), scent);
    }

    [Fact]
    public void RobotIgnoresLostMoveIfScentPresent()
    {
        var scent = new HashSet<(int, int, Orientation)>{ (0, 0, Orientation.S) };
        var robot = new Robot(new Position(0, 0, Orientation.S));
        robot.Execute("F", 5, 5, scent);
        Assert.Equal("0 0 S", robot.GetOutput());
    }
}

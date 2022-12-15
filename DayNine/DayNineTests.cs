using System.Security.Cryptography;
using FluentAssertions;

namespace DayNine;

public class DayNineTests
{
    [Fact]
    public void the_tail_can_follow_the_head_from_specific_position()
    {
        var initialHead = new Location(1, 3);
        var initialTail = new Location(2, 4);

        var sut = new Rope(initialHead, initialTail);

        sut.ApplyHeadMove("R 4");

        sut.TailLocation.Should().Be(new Location(4, 3));
    }

    [Fact]
    public void the_tail_can_follow_the_head_moves()
    {
        var initialHead = new Location(0, 0);
        var initialTail = new Location(0, 0);

        var sut = new Rope(initialHead, initialTail);

        sut.ApplyHeadMove("R 4");
        sut.TailLocation.Should().Be(new Location(3, 0));

        sut.ApplyHeadMove("U 4");
        sut.TailLocation.Should().Be(new Location(4, 3));

        sut.ApplyHeadMove("L 3");
        sut.TailLocation.Should().Be(new Location(2, 4));

        sut.ApplyHeadMove("D 1");
        sut.HeadLocation.Should().Be(new Location(1, 3));
        sut.TailLocation.Should().Be(new Location(2, 4));

        sut.ApplyHeadMove("R 4");
        sut.TailLocation.Should().Be(new Location(4, 3));

        sut.ApplyHeadMove("D 1");
        sut.TailLocation.Should().Be(new Location(4, 3));

        sut.ApplyHeadMove("L 5");
        sut.TailLocation.Should().Be(new Location(1, 2));

        sut.ApplyHeadMove("R 2");
        sut.TailLocation.Should().Be(new Location(1, 2));
    }

    [Fact]
    public void it_can_count_the_unique_locations_that_the_tail_visited()
    {
        var initialHead = new Location(0, 0);
        var initialTail = new Location(0, 0);

        var sut = new Rope(initialHead, initialTail);

        sut.ApplyHeadMove("R 4");
        sut.ApplyHeadMove("U 4");
        sut.ApplyHeadMove("L 3");
        sut.ApplyHeadMove("D 1");
        sut.ApplyHeadMove("R 4");
        sut.ApplyHeadMove("D 1");
        sut.ApplyHeadMove("L 5");
        sut.ApplyHeadMove("R 2");

        sut.NumberOfUniqueLocationsTailVisited().Should().Be(13);
    }
}

public record Location(int x, int y)
{
    public Location Apply(Move move)
    {
        return new Location(x + move.x, y + move.y);
    }

    public Location Follow(Location otherLocation)
    {
        var distance = Math.Sqrt(Math.Pow(x - otherLocation.x, 2) + Math.Pow(y - otherLocation.y, 2));

        if (distance <= 1.5) return this;

        var xDifference = otherLocation.x - x;
        var yDifference = otherLocation.y - y;

        return new Location(Math.Abs(xDifference) > 0 ? x + Math.Sign(xDifference) : x, Math.Abs(yDifference) > 0 ? y + Math.Sign(yDifference) : y);
    }
}

public record Move(int x, int y)
{
}

public class Rope
{
    private Location headLocation;
    private readonly List<Location> tailLocations = new();

    public Rope(Location headLocation, Location tailLocation)
    {
        this.headLocation = headLocation;
        tailLocations.Add(tailLocation);
    }

    public Location HeadLocation => headLocation;
    public Location TailLocation => tailLocations.Last();

    public void ApplyHeadMove(string moveInstruction)
    {
        var token = moveInstruction.Split(" ");
        var direction = token[0];
        var distance = int.Parse(token[1]);

        var move = ToMove(direction);

        Enumerable.Range(1, distance).ForEach(_ => MakeMove(move));
    }

    private static Move ToMove(string direction) => direction switch
    {
        "R" => new Move(1, 0),
        "L" => new Move(-1, 0),
        "U" => new Move(0, 1),
        "D" => new Move(0, -1),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid direction: {direction}")
    };

    private void MakeMove(Move move)
    {
        headLocation = headLocation.Apply(move);
        tailLocations.Add(TailLocation.Follow(headLocation));
    }

    public int NumberOfUniqueLocationsTailVisited()
    {
        return tailLocations.Distinct().Count();
    }
}
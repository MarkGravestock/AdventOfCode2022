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

        var sut = new TwoKnotRope(initialHead, initialTail);

        sut.ApplyHeadMove("R 4");

        sut.TailLocation.Should().Be(new Location(4, 3));
    }

    [Fact]
    public void the_tail_can_follow_the_head_moves()
    {
        var initialHead = new Location(0, 0);
        var initialTail = new Location(0, 0);

        var sut = new TwoKnotRope(initialHead, initialTail);

        sut.ApplyHeadMove("R 4");
        sut.TailLocation.Should().Be(new Location(3, 0));

        sut.ApplyHeadMove("U 4");
        sut.TailLocation.Should().Be(new Location(4, 3));

        sut.ApplyHeadMove("L 3");
        sut.TailLocation.Should().Be(new Location(2, 4));

        sut.ApplyHeadMove("D 1");
        sut.HeadKnotLocation.Should().Be(new Location(1, 3));
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

        var sut = new TwoKnotRope(initialHead, initialTail);

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

    [Fact]
    public void it_can_count_the_unique_locations_that_the_tail_visited_for_ten_knot_rope()
    {
        var initialLocation = new Location(0, 0);

        var sut = new TenKnotRope(initialLocation);

        sut.ApplyHeadMove("R 4");
        sut.ApplyHeadMove("U 4");
        sut.ApplyHeadMove("L 3");
        sut.ApplyHeadMove("D 1");
        sut.ApplyHeadMove("R 4");
        sut.ApplyHeadMove("D 1");
        sut.ApplyHeadMove("L 5");
        sut.ApplyHeadMove("R 2");

        sut.NumberOfUniqueLocationsTailVisited().Should().Be(1);
    }

    [Fact]
    public void it_can_count_the_unique_locations_that_the_tail_visited_for_ten_knot_rope_where_the_tail_moves()
    {
        var initialLocation = new Location(0, 0);

        var sut = new TenKnotRope(initialLocation);

        sut.ApplyHeadMove("R 5");
        sut.ApplyHeadMove("U 8");
        sut.ApplyHeadMove("L 8");
        sut.ApplyHeadMove("D 3");
        sut.ApplyHeadMove("R 17");
        sut.ApplyHeadMove("D 10");
        sut.ApplyHeadMove("L 25");
        sut.ApplyHeadMove("U 20");

        sut.NumberOfUniqueLocationsTailVisited().Should().Be(36);
    }

}
using Common;
using FluentAssertions;

namespace DayNine;

public class DayNineActualTest
{
    [Fact]
    public void it_can_count_the_unique_locations_that_the_tail_visited_for_part_one()
    {
        FileReader fileReader = new("input.txt");

        var initialHead = new Location(0, 0);
        var initialTail = new Location(0, 0);

        var sut = new Rope(initialHead, initialTail);

        fileReader.Lines().ForEach(moveInstruction => sut.ApplyHeadMove(moveInstruction));

        sut.NumberOfUniqueLocationsTailVisited().Should().Be(6406);
    }
}
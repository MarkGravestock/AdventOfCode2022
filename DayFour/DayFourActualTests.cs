using Common;
using FluentAssertions;

namespace DayFour;

public class DayFourActualTests
{
    [Fact]
    public void it_can_assignments_fully_contained_in_the_other()
    {
        var fileReader = new FileReader("input.txt");

        new AssignmentChecker().TotalSectionsCompletelyContainingTheOther(fileReader.Lines()).Should().Be(584);
    }

}
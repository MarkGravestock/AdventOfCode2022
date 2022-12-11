using FluentAssertions;

namespace DayFour;

public class DayFourPartOneTests
{
    [Theory]
    [InlineData("2-4,2-4", true)]
    [InlineData("2-4,6-8", false)]
    [InlineData("2-3,4-5", false)]
    [InlineData("5-7,7-9", false)]
    [InlineData("2-8,3-7", true)]
    [InlineData("6-6,4-6", true)]
    [InlineData("2-6,4-8", false)]
    public void it_can_check_if_one_elfs_sections_completely_contain_the_others(string sectionAssignments, bool contains)
    {
        new AssignmentChecker().DoesSectionCompletelyContainTheOther(sectionAssignments).Should().Be(contains);
    }

    private string[] sectionAssignments = {
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8"
    };

    [Fact]
    public void it_can_total_sections_completely_contain_the_others()
    {
        new AssignmentChecker().TotalSectionsCompletelyContainingTheOther(sectionAssignments).Should().Be(2);
    }

}
using Common;
using FluentAssertions;

namespace DayThree;

public class DayThreeActualTest
{
    [Fact]
    public void it_can_find_the_total_priorities_of_all_common_item()
    {
        var fileReader = new FileReader("input.txt");

        new RucksackPacker().FindTotalPriority(fileReader.Lines()).Should().Be(7872);
    }

    [Fact]
    public void it_can_find_the_total_priorities_of_all_group_common_item()
    {
        var fileReader = new FileReader("input.txt");

        new RucksackPacker().FindTotalGroupPriorities(fileReader.Lines()).Should().Be(2497);
    }

}
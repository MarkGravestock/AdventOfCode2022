using Common;
using FluentAssertions;

namespace DayThree;

public class DayThreePartOneActualTest
{
    [Fact]
    public void it_can_find_the_total_priorities_of_all_common_item()
    {
        var fileReader = new FileReader("input.txt");

        new RucksackPacker().FindTotalPriority(fileReader.Lines()).Should().Be(7872);
    }

}
using Common;
using FluentAssertions;

namespace DayEight;

public class DayEightActualTest
{
    [Fact]
    public void calculate_how_many_trees_are_visible_for_part_one()
    {
        FileReader fileReader = new("input.txt");

        var sut = new Forest(fileReader.Lines());

        sut.CalculateTotalVisibleTrees().Should().Be(1662);
    }

}
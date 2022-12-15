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

    [Fact]
    public void calculate_max_scenic_score_for_part_two()
    {
        FileReader fileReader = new("input.txt");

        var sut = new Forest(fileReader.Lines());

        sut.CalculateMaxScenicScoreForForest().Should().Be(537600);
    }
}
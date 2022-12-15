using System.Text;
using FluentAssertions;

namespace DayEight;

public class DayEightTest
{
    private readonly string[] forestDefinition;
    private readonly Forest sut;

    public DayEightTest()
    {
        forestDefinition = new[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };
        sut = new Forest(forestDefinition);
    }

    [Fact]
    public void calculate_how_trees_are_visible()
    {
        sut.CalculateTotalVisibleTrees().Should().Be(21);
    }

    [Fact]
    public void calculate_viewing_distance_from_tree_to_left()
    {
        sut.CalculateViewingDistanceToLeftFor(2, 1).Should().Be(1);
    }

    [Fact]
    public void calculate_viewing_distance_from_tree_to_right()
    {
        sut.CalculateViewingDistanceToRightFor(2, 1).Should().Be(2);
    }

    [Fact]
    public void calculate_viewing_distance_from_tree_to_top()
    {
        sut.CalculateViewingDistanceToTopFor(2, 1).Should().Be(1);
    }

    [Fact]
    public void calculate_viewing_distance_from_tree_to_bottom()
    {
        sut.CalculateViewingDistanceToBottomFor(2, 1).Should().Be(2);
    }

    [Fact]
    public void calculate_scenic_score_for_tree()
    {
        sut.CalculateScenicScoreFor(2, 1).Should().Be(4);
    }

    [Fact]
    public void calculate_scenic_score_for_another_tree()
    {
        sut.CalculateScenicScoreFor(2, 3).Should().Be(8);
    }

    [Fact]
    public void calculate_scenic_score_for_forest()
    {
        sut.CalculateMaxScenicScoreForForest().Should().Be(8);
    }


}
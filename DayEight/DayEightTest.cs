using System.Text;
using FluentAssertions;

namespace DayEight;

public class DayEightTest
{
    [Fact]
    public void calculate_how_trees_are_visible()
    {
        var forestDefinition = new[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

        var sut = new Forest(forestDefinition);
        
        sut.CalculateTotalVisibleTrees().Should().Be(21);
    }
}
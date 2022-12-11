using FluentAssertions;

namespace DayTwo;

public class DayTwoActualTest
{
    [Fact]
    public void it_can_calculate_the_result_of_a_game()
    {
        new RockPaperScissorsGame("input.txt").CalculateGameScore().Should().Be(13682);
    }

    [Fact]
    public void it_can_calculate_the_real_result_of_a_game()
    {
        new RockPaperScissorsGame("input.txt").CalculateGameScoreForRealStrategy().Should().Be(12881);
    }
}
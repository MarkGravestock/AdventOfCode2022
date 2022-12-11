using FluentAssertions;

namespace DayTwo;

public class DayTwoTests
{
    [Fact]
    public void it_can_calculate_the_result_of_one_round()
    {
        new RockPaperScissorsGame("ignored").CalculateRoundScoreFor("A Y").Should().Be(8);
    }

    [Fact]
    public void it_can_calculate_the_result_of_a_game()
    {
        new RockPaperScissorsGame("test.txt").CalculateGameScore().Should().Be(15);
    }

}

internal enum Outcome
{
    Lost = 0,
    Draw = 3,
    Won = 6
}

internal enum Play
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}
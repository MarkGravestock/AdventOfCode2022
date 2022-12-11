using FluentAssertions;

namespace DayTwo;

public class DayTwoTests
{
    [Fact]
    public void it_can_calculate_the_result_of_one_round()
    {
        new RockScissorsPaperGame("ignored").CalculateRoundScoreFor("A Y").Should().Be(8);
    }

    [Fact]
    public void it_can_calculate_the_result_of_a_game()
    {
        new RockScissorsPaperGame("test.txt").CalculateGameScore().Should().Be(15);
    }

}

public class RockScissorsPaperGame
{
    public RockScissorsPaperGame(string fileName)
    {
        this.fileName = fileName;
    }

    public IEnumerable<string> Lines()
    {
        using var sr = new StreamReader(fileName);
        var text = sr.ReadToEnd();
        var lines = text.Split("\n");
        return lines.Select(x => x.Trim()).Where(x => x != String.Empty);
    }


    private Dictionary<string, Play> plays = new()
    {
        { "A", Play.Rock },
        { "B", Play.Paper },
        { "C", Play.Scissors },
        { "X", Play.Rock },
        { "Y", Play.Paper },
        { "Z", Play.Scissors },
    };

    private readonly string fileName;

    public int CalculateRoundScoreFor(string round)
    {
        var opponentPlay = plays[round.Substring(0, 1)];
        var myPlay = plays[round.Substring(2, 1)];

        var outcome = IsDraw(opponentPlay, myPlay) ? Outcome.Draw :
            OpponentHasWon(opponentPlay, myPlay) ? Outcome.Lost : Outcome.Won;

        return (int)outcome + (int)myPlay;
    }

    private static bool IsDraw(Play opponentPlay, Play myPlay)
    {
        return opponentPlay == myPlay;
    }

    private bool OpponentHasWon(Play opponentPlay, Play myPlay)
    {
        return opponentPlay == Play.Rock && myPlay != Play.Paper ||
               opponentPlay == Play.Paper && myPlay == Play.Rock ||
               opponentPlay == Play.Scissors && myPlay == Play.Paper;
    }

    public int CalculateGameScore()
    {
        return Lines().Select(CalculateRoundScoreFor).Sum();
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
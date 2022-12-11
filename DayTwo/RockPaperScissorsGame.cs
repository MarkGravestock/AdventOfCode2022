using Common;

namespace DayTwo;

public class RockPaperScissorsGame
{
    private readonly FileReader fileReader;


    public RockPaperScissorsGame(string fileName)
    {
        fileReader = new FileReader(fileName);
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

    private Dictionary<string, Outcome> outcomes = new()
    {
        { "X", Outcome.Lost },
        { "Y", Outcome.Draw },
        { "Z", Outcome.Won },
    };

    private readonly Dictionary<Play, Play> loseStrategy = new()
    {
        { Play.Rock, Play.Scissors },
        { Play.Paper, Play.Rock },
        { Play.Scissors, Play.Paper },
    };

    private readonly Dictionary<Play, Play> winStrategy = new()
    {
        { Play.Rock, Play.Paper },
        { Play.Paper, Play.Scissors },
        { Play.Scissors, Play.Rock },
    };

    public int CalculateRoundScoreFor(string round)
    {
        var opponentPlay = plays[round.Substring(0, 1)];
        var myPlay = plays[round.Substring(2, 1)];

        var outcome = IsDraw(opponentPlay, myPlay) ? Outcome.Draw :
            OpponentHasWon(opponentPlay, myPlay) ? Outcome.Lost : Outcome.Won;

        return (int)outcome + (int)myPlay;
    }

    public int CalculateRoundScoreForRealStrategy(string round)
    {
        var opponentPlay = plays[round.Substring(0, 1)];
        var outcome = outcomes[round.Substring(2, 1)];

        var myPlay = outcome == Outcome.Draw ? opponentPlay : outcome == Outcome.Lost ? loseStrategy[opponentPlay] : winStrategy[opponentPlay];

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
        return fileReader.Lines().Select(CalculateRoundScoreFor).Sum();
    }

    public int CalculateGameScoreForRealStrategy()
    {
        return fileReader.Lines().Select(CalculateRoundScoreForRealStrategy).Sum();
    }

}
namespace DayNine;

public class Rope
{
    private Location headLocation;
    private readonly List<Location> tailLocations = new();

    public Rope(Location headLocation, Location tailLocation)
    {
        this.headLocation = headLocation;
        tailLocations.Add(tailLocation);
    }

    public Location HeadLocation => headLocation;
    public Location TailLocation => tailLocations.Last();

    public void ApplyHeadMove(string moveInstruction)
    {
        var token = moveInstruction.Split(" ");
        var direction = token[0];
        var distance = int.Parse(token[1]);

        var move = ToMove(direction);

        Enumerable.Range(1, distance).ForEach(_ => MakeMove(move));
    }

    private static Move ToMove(string direction) => direction switch
    {
        "R" => new Move(1, 0),
        "L" => new Move(-1, 0),
        "U" => new Move(0, 1),
        "D" => new Move(0, -1),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid direction: {direction}")
    };

    private void MakeMove(Move move)
    {
        headLocation = headLocation.Apply(move);
        tailLocations.Add(TailLocation.Follow(headLocation));
    }

    public int NumberOfUniqueLocationsTailVisited()
    {
        return tailLocations.Distinct().Count();
    }
}
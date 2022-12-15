namespace DayNine;

public class TwoKnotRope
{
    private Location headKnotLocation;
    private readonly List<Location> tailKnotLocations = new();

    public TwoKnotRope(Location headKnotLocation, Location tailLocation)
    {
        this.headKnotLocation = headKnotLocation;
        tailKnotLocations.Add(tailLocation);
    }

    public Location HeadKnotLocation => headKnotLocation;
    public Location TailLocation => tailKnotLocations.Last();

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
        headKnotLocation = headKnotLocation.Apply(move);
        tailKnotLocations.Add(TailLocation.Follow(headKnotLocation));
    }

    public int NumberOfUniqueLocationsTailVisited()
    {
        return tailKnotLocations.Distinct().Count();
    }
}
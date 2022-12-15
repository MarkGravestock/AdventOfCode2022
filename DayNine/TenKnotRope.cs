namespace DayNine;

public class TenKnotRope
{
    private List<Location> knotLocations = new();
    private readonly List<Location> tailKnotLocations = new();

    public TenKnotRope(Location initialLocation)
    {
        Enumerable.Range(1, 10).ForEach(_ => knotLocations.Add(initialLocation));
        tailKnotLocations.Add(initialLocation);
    }

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
        knotLocations[0] = knotLocations[0].Apply(move);
        var lastKnotLocation = knotLocations[0];

        for (int i = 1; i < knotLocations.Count; i++)
        {
            knotLocations[i] = knotLocations[i].Follow(lastKnotLocation);
            lastKnotLocation = knotLocations[i];
        }

        tailKnotLocations.Add(lastKnotLocation);
    }

    public int NumberOfUniqueLocationsTailVisited()
    {
        return tailKnotLocations.Distinct().Count();
    }
}
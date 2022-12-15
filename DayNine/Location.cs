namespace DayNine;

public record Location(int x, int y)
{
    public Location Apply(Move move)
    {
        return new Location(x + move.x, y + move.y);
    }

    public Location Follow(Location otherLocation)
    {
        var distance = Math.Sqrt(Math.Pow(x - otherLocation.x, 2) + Math.Pow(y - otherLocation.y, 2));

        if (distance <= 1.5) return this;

        var xDifference = otherLocation.x - x;
        var yDifference = otherLocation.y - y;

        return new Location(Math.Abs(xDifference) > 0 ? x + Math.Sign(xDifference) : x, Math.Abs(yDifference) > 0 ? y + Math.Sign(yDifference) : y);
    }
}
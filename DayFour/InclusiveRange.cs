namespace DayFour;

public record InclusiveRange(int start, int end)
{
    public bool Includes(InclusiveRange other)
    {
        return start <= other.start && end >= other.end;
    }

    public bool Overlaps(InclusiveRange other)
    {
        return start <= other.end && end >= other.start;
    }
}
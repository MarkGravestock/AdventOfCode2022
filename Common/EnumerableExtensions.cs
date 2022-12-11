namespace Common;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> items, int partitionSize)
    {
        List<T> partition = new List<T>(partitionSize);
        foreach (T item in items)
        {
            partition.Add(item);
            if (partition.Count == partitionSize)
            {
                yield return partition;
                partition = new List<T>(partitionSize);
            }
        }
        // Cope with items.Count % partitionSize != 0
        if (partition.Count > 0) yield return partition;
    }
}
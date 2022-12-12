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

    public static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> list, int windowSize)
    {
        //Checks elided
        var arr = new T[windowSize];
        int r = windowSize - 1, i = 0;
        using(var e = list.GetEnumerator())
        {
            while(e.MoveNext())
            {
                arr[i] = e.Current;
                i = (i + 1) % windowSize;
                if(r == 0)
                    yield return ArrayInit<T>(windowSize, j => arr[(i + j) % windowSize]);
                else
                    r = r - 1;
            }
        }
    }

    private static T[] ArrayInit<T>(int size, Func<int, T> func)
    {
        var output = new T[size];
        for(var i = 0; i < size; i++) output[i] = func(i);
        return output;
    }
}
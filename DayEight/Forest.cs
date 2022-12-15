using System.Text;

namespace DayEight;

public class Forest
{
    private readonly string[] forestDefinition;
    private readonly int[,] forest;

    public Forest(IEnumerable<string> forestDefinition)
    {
        this.forestDefinition = forestDefinition.ToArray();
        forest = new int[this.forestDefinition[0].Length, this.forestDefinition.Length];
        this.forestDefinition.ForEach((row, y) => row.ToCharArray().ForEach((height, x) => forest[x,y] = int.Parse(height.ToString())));
    }

    public int CalculateTotalVisibleTrees()
    {
        var isVisible = new bool[forestDefinition[0].Length, forestDefinition.Length];

        visibleFromLeft(forestDefinition, forest, isVisible);
        visibleFromRight(forestDefinition, forest, isVisible);
        visibleFromTop(forestDefinition, forest, isVisible);
        visibleFromBottom(forestDefinition, forest, isVisible);

        return calculateVisibleTotal(forestDefinition, isVisible);

    }
    private static int calculateVisibleTotal(string[] forestDefinition, bool[,] isVisible)
    {
        int visibleTotal = 0;
        for (int y = 0; y < forestDefinition.Length; y++)
        {
            for (int x = 0; x < forestDefinition[0].Length; x++)
            {
                if (isVisible[x, y])
                {
                    visibleTotal++;
                }
            }
        }

        return visibleTotal;
    }

    private static void visibleFromLeft(string[] forestDefinition, int[,] forest, bool[,] isVisible)
    {
        for (int y = 0; y < forestDefinition.Length; y++)
        {
            int currentMaxHeight = -1;

            for (int x = 0; x < forestDefinition[0].Length; x++)
            {
                if (forest[x, y] > currentMaxHeight)
                {
                    currentMaxHeight = forest[x, y];
                    isVisible[x, y] |= true;
                }
            }
        }
    }

    private static void visibleFromRight(string[] forestDefinition, int[,] forest, bool[,] isVisible)
    {
        for (int y = 0; y < forestDefinition.Length; y++)
        {
            int currentMaxHeight = -1;

            for (int x = forestDefinition[0].Length - 1; x > 0; x--)
            {
                if (forest[x, y] > currentMaxHeight)
                {
                    currentMaxHeight = forest[x, y];
                    isVisible[x, y] |= true;
                }
            }
        }
    }

    private static void visibleFromTop(string[] forestDefinition, int[,] forest, bool[,] isVisible)
    {
        for (int x = 0; x < forestDefinition[0].Length; x++)
        {
            int currentMaxHeight = -1;

            for (int y = 0; y < forestDefinition.Length; y++)
            {
                if (forest[x, y] > currentMaxHeight)
                {
                    currentMaxHeight = forest[x, y];
                    isVisible[x, y] |= true;
                }
            }
        }
    }

    private static void visibleFromBottom(string[] forestDefinition, int[,] forest, bool[,] isVisible)
    {
        for (int x = 0; x < forestDefinition[0].Length; x++)
        {
            int currentMaxHeight = -1;

            for (int y = forestDefinition.Length - 1; y > 0 ; y--)
            {
                if (forest[x, y] > currentMaxHeight)
                {
                    currentMaxHeight = forest[x, y];
                    isVisible[x, y] |= true;
                }
            }
        }
    }

    public int CalculateViewingDistanceToLeftFor(int x, int y)
    {
        return CalculateViewingDistanceFor(x, y, -1);
    }

    public int CalculateViewingDistanceToRightFor(int x, int y)
    {
        return CalculateViewingDistanceFor(x, y, 1);
    }

    public int CalculateViewingDistanceFor(int x, int y, int xDelta, int yDelta = 0)
    {
        int currentHeight = forest[x, y];
        int viewingDistance = 0;

        while (x > 0 && x < forest.GetLength(0) - 1 && y > 0 && y < forest.GetLength(1) - 1)
        {
            x += xDelta;
            y += yDelta;
            viewingDistance++;
            if (forest[x, y] >= currentHeight)
            {
                break;
            }
        }

        return viewingDistance;
    }

    public int CalculateViewingDistanceToTopFor(int x, int y)
    {
        return CalculateViewingDistanceFor(x, y, 0, -1);
    }

    public int CalculateViewingDistanceToBottomFor(int x, int y)
    {
        return CalculateViewingDistanceFor(x, y, 0, 1);
    }

    public int CalculateScenicScoreFor(int x, int y)
    {
        return CalculateViewingDistanceToLeftFor(x, y) *
               CalculateViewingDistanceToRightFor(x, y) *
               CalculateViewingDistanceToTopFor(x, y) *
               CalculateViewingDistanceToBottomFor(x, y);
    }

    public int CalculateMaxScenicScoreForForest()
    {
        IEnumerable<(int, int)> trees = Enumerable.Range(0,forest.GetLength(0)).SelectMany((row, i) => Enumerable.Range(0, forest.GetLength(1)).Select((_, j) => (i, j)));

        return trees.Select(location => CalculateScenicScoreFor(location.Item1, location.Item2)).Max();
    }

}
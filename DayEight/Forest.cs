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
        int currentHeight = forest[x, y];
        int viewingDistance = 0;

        while (x > 0)
        {
            x--;
            viewingDistance++;
            if (forest[x, y] >= currentHeight)
            {
                break;
            }
        }

        return viewingDistance;
    }

    public int CalculateViewingDistanceToRightFor(int x, int y)
    {
        int currentHeight = forest[x, y];
        int viewingDistance = 0;

        while (x < forest.GetLength(0))
        {
            x++;
            viewingDistance++;
            if (forest[x, y] >= currentHeight)
            {
                break;
            }
        }

        return viewingDistance;
    }
}
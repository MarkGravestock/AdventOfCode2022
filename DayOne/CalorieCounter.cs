namespace DayOne;

public static class CalorieCounter
{
    public static string[] Lines()
    {
        using var sr = new StreamReader("test.txt");
        var text = sr.ReadToEnd();
        var lines = text.Split("\n");
        return lines;
    }

    public static int ElfWithMaximumCalories()
    {
        int elf = 1;
        int maxTotal = 0;
        int elfWithMaximum = 0;

        foreach (var total in TotalPerElf())
        {
            if (total > maxTotal)
            {
                maxTotal = total;
                elfWithMaximum = elf;
            }

            elf++;
        }

        return elfWithMaximum;
    }

    public static IList<int> TotalPerElf()
    {
        var totalPerElf = new List<int>();
        var lines = Lines();
        var currentElfTotal = 0;

        foreach (var inputLine in lines)
        {
            var line = inputLine.Trim();

            if (line == String.Empty)
            {
                totalPerElf.Add(currentElfTotal);
                currentElfTotal = 0;
                continue;
            }

            currentElfTotal += int.Parse(line);
        }

        return totalPerElf;
    }
}
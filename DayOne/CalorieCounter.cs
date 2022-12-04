namespace DayOne;

public readonly record struct ElfCalories(int Number, int Calories);

public class CalorieCounter
{
    private readonly string fileName;

    public CalorieCounter(string fileName)
    {
        this.fileName = fileName;
    }

    public string[] Lines()
    {
        using var sr = new StreamReader(fileName);
        var text = sr.ReadToEnd();
        var lines = text.Split("\n");
        return lines;
    }

    public ElfCalories ElfWithMaximumCalories()
    {
        int elfNumber = 1;
        int maxTotal = 0;
        ElfCalories elfWithMaximum = new ElfCalories(0, 0);

        foreach (var total in TotalPerElf())
        {
            if (total > maxTotal)
            {
                maxTotal = total;
                elfWithMaximum = new ElfCalories(elfNumber, maxTotal);

            }

            elfNumber++;
        }

        return elfWithMaximum;
    }

    public IList<int> TotalPerElf()
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
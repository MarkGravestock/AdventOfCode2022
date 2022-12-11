namespace DayThree;

public class RucksackPacker
{
    public int FindTotalPriority(IEnumerable<string> contents)
    {
        return contents.Select(FindPriorityOfCommonItemIn).Sum();
    }

    public int FindPriorityOfCommonItemIn(string contents)
    {
        var commonItem = FindCommonItemIn(contents);
        int characterValue = commonItem.ToUpper()[0];
        return (characterValue - 64) + (Char.IsUpper(commonItem[0]) ? 26 : 0);
    }

    public string FindCommonItemIn(string contents)
    {
        var firstCompartmentContents = FirstCompartmentContentsOf(contents);
        var secondCompartmentContents = SecondCompartmentContentsOf(contents);

        var common = firstCompartmentContents.ToCharArray().Intersect(secondCompartmentContents.ToCharArray());

        return common.Distinct().Single().ToString();
    }

    public string FirstCompartmentContentsOf(string contents)
    {
        var eachCompartmentItems = contents.Length / 2;
        return contents.Substring(0, eachCompartmentItems);
    }

    public string SecondCompartmentContentsOf(string contents)
    {
        var eachCompartmentItems = contents.Length / 2;
        return contents.Substring(eachCompartmentItems, eachCompartmentItems);
    }
}
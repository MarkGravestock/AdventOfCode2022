using Common;

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
        return CalculatePriority(commonItem);
    }

    private static int CalculatePriority(string commonItem)
    {
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

    public string FindGroupCommonItemIn(IEnumerable<string> groupItems)
    {
        var allGroupItems = groupItems.ToArray();

        var commonItem = allGroupItems[0].ToCharArray()
            .Intersect(allGroupItems[1].ToCharArray())
            .Intersect(allGroupItems[2].ToCharArray());

        return commonItem.Single().ToString();
    }

    public int FindTotalGroupPriorities(IEnumerable<string> rucksacks)
    {
        return rucksacks.Partition(3)
            .Select(FindGroupCommonItemIn)
            .Select(CalculatePriority)
            .Sum();
    }
}
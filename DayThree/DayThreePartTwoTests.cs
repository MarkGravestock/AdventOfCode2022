using FluentAssertions;
using static Common.EnumerableExtensions;

namespace DayThree;

public class DayThreePartTwoTests
{
    private RucksackPacker sut = new();

    private string[] rucksacks = new[] {
        "vJrwpWtwJgWrhcsFMMfFFhFp",
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        "PmmdzqPrVvPwwTWBwg",
        "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        "ttgJtRGJQctTZtZT",
        "CrZsJsPPZsGzwwsLwLmpwMDw"
    };

    [Fact]
    public void it_can_partition_into_groups()
    {
       rucksacks.Partition(3).Should().HaveCount(2);
    }

    [Fact]
    public void it_can_find_group_common_item()
    {
        rucksacks.Partition(3).Select(rucksack => sut.FindGroupCommonItemIn(rucksack)).Should().BeEquivalentTo(new[]{"r", "Z"});
    }

    [Fact]
    public void it_can_find_total_priorities()
    {
        sut.FindTotalGroupPriorities(rucksacks).Should().Be(70);
    }

}
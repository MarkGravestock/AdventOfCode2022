using FluentAssertions;

namespace DayThree;

public class DayThreePartOneTests
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
    public void it_can_find_common_item()
    {
        sut.FindCommonItemIn("vJrwpWtwJgWrhcsFMMfFFhFp").Should().Be("p");
    }

    [Fact]
    public void it_can_find_priority_of_common_item()
    {
        sut.FindPriorityOfCommonItemIn("PmmdzqPrVvPwwTWBwg").Should().Be(42);
    }

    [Fact]
    public void it_can_find_all_common_item()
    {
        rucksacks.Select(contents => sut.FindCommonItemIn(contents)).Should().Contain("p", "L", "P", "v", "t", "s");
    }

    [Fact]
    public void it_can_find_the_priorities_of_all_common_item()
    {
        rucksacks.Select(contents => sut.FindPriorityOfCommonItemIn(contents)).Should().BeEquivalentTo(new[] {16, 38, 42, 22, 20, 19});
    }

    [Fact]
    public void it_can_find_the_total_priorities_of_all_common_item()
    {
        sut.FindTotalPriority(rucksacks).Should().Be(157);
    }

    [Fact]
    public void it_can_find_the_first_compartments_items()
    {
        sut.FirstCompartmentContentsOf("vJrwpWtwJgWrhcsFMMfFFhFp").Should().Be("vJrwpWtwJgWr");
    }

    [Fact]
    public void it_can_find_the_second_compartments_items()
    {
        sut.SecondCompartmentContentsOf("vJrwpWtwJgWrhcsFMMfFFhFp").Should().Be("hcsFMMfFFhFp");
    }

}
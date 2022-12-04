using FluentAssertions;

namespace DayOne;

public class DayOneActualTest
{
    [Fact]
    public void it_can_find_the_maximum_calories_of_one_elf_for_the_real_file()
    {
        new CalorieCounter("input.txt").ElfWithMaximumCalories().Calories.Should().Be(69_883);
    }

    [Fact]
    public void it_can_find_the_total_maximum_calories_for_the_top_3_elves_for_the_real_file()
    {
        new CalorieCounter("input.txt").TotalCaloriesOfTopThreeElves().Should().Be(207_576);
    }
}
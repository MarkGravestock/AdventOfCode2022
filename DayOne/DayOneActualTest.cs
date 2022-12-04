using FluentAssertions;

namespace DayOne;

public class DayOneActualTest
{
    [Fact]
    public void it_can_find_the_maximum_calories_for_the_real_file()
    {
        new CalorieCounter("input.txt").ElfWithMaximumCalories().Calories.Should().Be(69883);
    }
}
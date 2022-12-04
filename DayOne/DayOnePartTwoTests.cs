using FluentAssertions;

namespace DayOne;

public class DayOnePartTwoTests
{
    private readonly CalorieCounter sut = new CalorieCounter("test.txt");

    [Fact]
    public void the_three_elves_with_the_most_calories_are()
    {
        sut.TotalPerElf().OrderByDescending(total => total).Take(3).Should().Equal(24_000, 11_000, 10_000);
    }

    [Fact]
    public void the_three_elves_with_the_most_calories_have_a_total_of()
    {
        sut.TotalPerElf().OrderByDescending(elfTotal => elfTotal).Take(3).Sum(elfTotal => elfTotal).Should().Be(45_000);
    }

    [Fact]
    public void the_three_elves_with_the_most_calories_have_the_expected_total()
    {
        sut.TotalCaloriesOfTopThreeElves().Should().Be(45_000);
    }
}
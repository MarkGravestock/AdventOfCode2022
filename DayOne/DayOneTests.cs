using FluentAssertions;

namespace DayOne;

public class DayOneTests
{
    [Fact]
    public void the_elf_with_the_most_calories_is_number_four()
    {
        CalorieCounter.ElfWithMaximumCalories().Should().Be(4);
    }

    [Fact]
    public void it_can_read_the_test_file()
    {
        var lines = CalorieCounter.Lines();
        lines.Count().Should().Be(15);
    }

    [Fact]
    public void it_can_create_total_per_elf()
    {
        var totalPerElf = CalorieCounter.TotalPerElf();
        totalPerElf.Should().HaveCount(5);
    }

    [Fact]
    public void it_can_total_per_elf()
    {
        var totalPerElf = CalorieCounter.TotalPerElf();
        totalPerElf.Should().Equal(6000, 4000, 11000, 24000, 10000);
    }
}
using Common;
using FluentAssertions;

namespace DaySeven;

public class DaySevenActualTests
{
    [Fact] public void it_can_calculate_the_total_size_of_all_directories_with_the_given_size_for_part_one()
    {
        FileReader fileReader = new("input.txt");

        var fileSystem = new DaySevenTests.FileSystem();
        fileSystem.CreateFromInteractions(fileReader.Lines());

        fileSystem.SizeOfMatchingDirectories().Should().Be(1915606);
    }

    [Fact] public void it_can_calculate_the_smallest_directory_which_free_enough_space_for_part_two()
    {
        FileReader fileReader = new("input.txt");

        var fileSystem = new DaySevenTests.FileSystem();
        fileSystem.CreateFromInteractions(fileReader.Lines());

        fileSystem.SizeOfDirectoryToDelete().Should().Be(5025657);
    }
}
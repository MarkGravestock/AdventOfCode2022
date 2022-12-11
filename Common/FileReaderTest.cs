using FluentAssertions;

namespace Common;

public class FileReaderTest
{
    [Fact]
    public void it_reads_the_lines_in_a_file()
    {
        new FileReader("test.txt").Lines().Should().Contain("A Y", "B X", "C Z");
    }
}
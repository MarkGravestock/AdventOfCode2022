using Common;
using FluentAssertions;

namespace DaySix;

public class DaySixTest
{
    [Fact]
    public void it_finds_the_start_packet_marker()
    {
        var sut = new StartOfPacketMarkerFinder();
        sut.FindIn("mjqjpqmgbljsphdztnvjfqwrcgsmlb").Should().Be(7);
    }

    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void it_finds_the_start_packet_markers(string packet, int startOfMarkerCharacter)
    {
        var sut = new StartOfPacketMarkerFinder();
        sut.FindIn(packet).Should().Be(startOfMarkerCharacter);
    }

}

public class StartOfPacketMarkerFinder
{
    public int FindIn(string packet)
    {
        const int windowLength = 4;
        var results = packet.Windowed(windowLength)
            .Select((characters, index) => new {Characters = characters,Unique = characters.Distinct().Count(), Index = index})
            .First(res => res.Unique == 4);

        return results.Index + windowLength;
    }
}
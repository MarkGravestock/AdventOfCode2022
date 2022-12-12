using Common;
using FluentAssertions;

namespace DaySix;

public class DaySixTest
{
    [Fact]
    public void it_finds_the_start_packet_marker()
    {
        var sut = new PacketMarkerFinder();
        sut.FindStartOfPacketIn("mjqjpqmgbljsphdztnvjfqwrcgsmlb").Should().Be(7);
    }

    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void it_finds_the_start_packet_markers(string packet, int startOfMarkerCharacter)
    {
        var sut = new PacketMarkerFinder();
        sut.FindStartOfPacketIn(packet).Should().Be(startOfMarkerCharacter);
    }

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void it_finds_the_start_message_markers(string packet, int startOfMarkerCharacter)
    {
        var sut = new PacketMarkerFinder();
        sut.FindStartOfMessageIn(packet).Should().Be(startOfMarkerCharacter);
    }
}

public class PacketMarkerFinder
{
    public int FindStartOfMessageIn(string packet)
    {
        return FindIn(packet, 14);
    }

    public int FindStartOfPacketIn(string packet)
    {
        return FindIn(packet, 4);
    }

    private int FindIn(string packet, int uniqueCharacters = 4)
    {
        var results = packet.Windowed(uniqueCharacters)
            .Select((characters, index) => new {Characters = characters,Unique = characters.Distinct().Count(), Index = index})
            .First(res => res.Unique == uniqueCharacters);

        return results.Index + uniqueCharacters;
    }
}
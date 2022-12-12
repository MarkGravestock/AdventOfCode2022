using Common;
using FluentAssertions;

namespace DaySix;

public class DaySixActualTests
{
    private readonly FileReader fileReader = new("input.txt");
    private readonly PacketMarkerFinder sut = new();

    [Fact]
    public void it_can_find_the_start_of_packet_for_part_one()
    {
        var packet = fileReader.Lines().First();

        sut.FindStartOfPacketIn(packet).Should().Be(1702);
    }

    [Fact]
    public void it_can_find_the_start_of_message_for_part_two()
    {
        var packet = fileReader.Lines().First();

        sut.FindStartOfMessageIn(packet).Should().Be(3559);
    }

}
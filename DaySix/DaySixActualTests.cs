using Common;
using FluentAssertions;

namespace DaySix;

public class DaySixActualTests
{
    private readonly FileReader fileReader = new("input.txt");
    private readonly StartOfPacketMarkerFinder sut = new();

    [Fact]
    public void it_can_find_the_start_of_packet_for_part_one()
    {
        var packet = fileReader.Lines().First();

        sut.FindIn(packet).Should().Be(1702);
    }
}
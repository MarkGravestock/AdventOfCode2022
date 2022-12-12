using Common;
using FluentAssertions;

namespace DaySix;

public class DayFiveActualTests
{
    private readonly FileReader fileReader = new("input_instructions.txt");
    private readonly SupplyStacks sut = new(new []{"BVWTQNHD", "BWD", "CJWQST", "PTZNRJF", "TSMJVPG", "NTFWB", "NVHFQDLB", "RFPH", "HPNLBMSZ"});

    [Fact]
    public void it_can_process_all_moves_for_part_one()
    {
        fileReader.Lines().ForEach(line => sut.ProcessInstructionForCrateMover9000(line));

        sut.TopOfStacks().Should().Be("PSNRGBTFT");
    }

    [Fact]
    public void it_can_process_all_moves_for_part_two()
    {
        fileReader.Lines().ForEach(line => sut.ProcessInstructionForCrateMover9001(line));

        sut.TopOfStacks().Should().Be("BNTZFPMMW");
    }

}
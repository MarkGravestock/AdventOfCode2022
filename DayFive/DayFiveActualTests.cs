using Common;
using FluentAssertions;

namespace DayFive;

public class DayFiveActualTests
{
    [Fact]
    public void it_can_process_all_moves_for_part_one()
    {
        var fileReader = new FileReader("input_instructions.txt");

        var sut = new SupplyStacks(new []{"BVWTQNHD", "BWD", "CJWQST", "PTZNRJF", "TSMJVPG", "NTFWB", "NVHFQDLB", "RFPH", "HPNLBMSZ"});

        fileReader.Lines().ForEach(line => sut.ProcessInstructionForCrateMover9000(line));

        sut.TopOfStacks().Should().Be("PSNRGBTFT");
    }

    [Fact]
    public void it_can_process_all_moves_for_part_two()
    {
        var fileReader = new FileReader("input_instructions.txt");

        var sut = new SupplyStacks(new []{"BVWTQNHD", "BWD", "CJWQST", "PTZNRJF", "TSMJVPG", "NTFWB", "NVHFQDLB", "RFPH", "HPNLBMSZ"});

        fileReader.Lines().ForEach(line => sut.ProcessInstructionForCrateMover9001(line));

        sut.TopOfStacks().Should().Be("BNTZFPMMW");
    }

}
using System.Text;
using Common;
using FluentAssertions;

namespace DayTen;

public class DayTenTests
{
    [Fact]
    public void it_can_process_instructions()
    {
        var cpu = new Cpu();
        cpu.RunInstruction("noop");
        cpu.GetRegisterStateAtCycle(2).Should().Be(1);
        cpu.RunInstruction("addx 3");
        cpu.GetRegisterStateAtCycle(4).Should().Be(4);
        cpu.RunInstruction("addx -5");
        cpu.GetRegisterStateAtCycle(6).Should().Be(-1);
    }

    [Fact]
    public void it_can_calculate_signal_strengths()
    {
        FileReader fileReader = new("example_input.txt");

        var sut = new Cpu();

        fileReader.Lines().ForEach(cpuInstruction => sut.RunInstruction(cpuInstruction));

        sut.GetSignalStrengthAtCycle(20).Should().Be(420);
        sut.GetSignalStrengthAtCycle(60).Should().Be(1140);
        sut.GetSignalStrengthAtCycle(100).Should().Be(1800);
        sut.GetSignalStrengthAtCycle(140).Should().Be(2940);
        sut.GetSignalStrengthAtCycle(180).Should().Be(2880);
        sut.GetSignalStrengthAtCycle(220).Should().Be(3960);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(8, false)]
    [InlineData(9, true)]
    [InlineData(10, true)]
    [InlineData(11, false)]
    public void it_can_determine_pixel_state(int cycle, bool state)
    {
        FileReader fileReader = new("example_input.txt");

        var sut = new Cpu();

        fileReader.Lines().ForEach(cpuInstruction => sut.RunInstruction(cpuInstruction));

        sut.IsPixelOnForCycle(cycle).Should().Be(state);
    }

    [Fact]
    public void it_can_draw_pixel_state()
    {
        FileReader fileReader = new("example_input.txt");

        var sut = new Cpu();

        fileReader.Lines().ForEach(cpuInstruction => sut.RunInstruction(cpuInstruction));

        var expected = new StringBuilder();
        expected.AppendLine("##..##..##..##..##..##..##..##..##..##..");
        expected.AppendLine("###...###...###...###...###...###...###.");
        expected.AppendLine("####....####....####....####....####....");
        expected.AppendLine("#####.....#####.....#####.....#####.....");
        expected.AppendLine("######......######......######......####");
        expected.AppendLine("#######.......#######.......#######.....");

        sut.ShowCrtUptoCycle(240).Should().Be(expected.ToString());
    }

    [Fact]
    public void it_can_calculate_total_interesting_signal_strengths()
    {
        FileReader fileReader = new("example_input.txt");

        var sut = new Cpu();

        fileReader.Lines().ForEach(cpuInstruction => sut.RunInstruction(cpuInstruction));

        sut.GetTotalInterestingSignalStrengths().Should().Be(13140);
    }

}

public class Cpu
{
    private List<int> registerXAtCycle = new();

    public Cpu()
    {
        registerXAtCycle.Add(1);
    }

    public void RunInstruction(string instruction)
    {
        switch (instruction.Substring(0, 4))
        {
            case "noop":
                registerXAtCycle.Add(registerXAtCycle.Last());
                break;
            case "addx":
                registerXAtCycle.Add(registerXAtCycle.Last());
                var amountToAdd = int.Parse(instruction.Substring(4));
                registerXAtCycle.Add(registerXAtCycle.Last() + amountToAdd);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(instruction), $"Unknown instruction: {instruction}");
        }
    }

    public int GetRegisterStateAtCycle(int cycle)
    {
        return registerXAtCycle[cycle - 1];
    }

    public int GetSignalStrengthAtCycle(int cycle)
    {
        return cycle * GetRegisterStateAtCycle(cycle);
    }

    public int GetTotalInterestingSignalStrengths()
    {
        return Enumerable.Range(0, registerXAtCycle.Count() / 40).Select(x => GetSignalStrengthAtCycle(x * 40 + 20)).Sum();
    }

    public bool IsPixelOnForCycle(int cycle)
    {
        var position = (cycle - 1) % 40;
        var registerStateAtCycle = GetRegisterStateAtCycle(cycle);
        var startOfSprite = registerStateAtCycle - 1;
        var endOfSprite = registerStateAtCycle + 1;

        return (position >= startOfSprite && position <= endOfSprite);
    }

    public string ShowCrtUptoCycle(int finalCycle)
    {
        StringBuilder crt = new();

        Enumerable.Range(1, finalCycle).ForEach((cycle) =>
        {
            crt.Append(IsPixelOnForCycle(cycle) ? "#" : ".");
            if (cycle % 40 == 0)
            {
                crt.AppendLine();
            }
        });

        return crt.ToString();
    }
}
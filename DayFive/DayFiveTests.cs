using FluentAssertions;

namespace DayFive;

public class DayFiveTests
{
    private readonly SupplyStacks sut = new(new []{"NZ", "DCM", "P"});

    [Fact]
    public void it_can_create_the_stack()
    {
        sut.State().Should().BeEquivalentTo("NZ", "DCM", "P");
    }

    [Fact]
    public void it_can_process_a_move()
    {
        sut.ProcessInstructionForCrateMover9000("move 1 from 2 to 1");

        sut.State().Should().BeEquivalentTo("DNZ", "CM", "P");
    }

    [Fact]
    public void it_can_process_all_moves()
    {
        sut.ProcessInstructionForCrateMover9000("move 1 from 2 to 1");
        sut.ProcessInstructionForCrateMover9000("move 3 from 1 to 3");
        sut.ProcessInstructionForCrateMover9000("move 2 from 2 to 1");
        sut.ProcessInstructionForCrateMover9000("move 1 from 1 to 2");

        sut.State().Should().BeEquivalentTo("C", "M", "ZNDP");
        sut.TopOfStacks().Should().Be("CMZ");
    }

    [Fact]
    public void it_can_process_a_crane_mover_9001_move()
    {
        sut.ProcessInstructionForCrateMover9001("move 1 from 2 to 1");
        sut.ProcessInstructionForCrateMover9001("move 3 from 1 to 3");

        sut.State().Should().BeEquivalentTo("", "CM", "DNZP");
    }

    [Fact]
    public void it_can_process_all_crane_mover_9001_moves()
    {
        sut.ProcessInstructionForCrateMover9001("move 1 from 2 to 1");
        sut.ProcessInstructionForCrateMover9001("move 3 from 1 to 3");
        sut.ProcessInstructionForCrateMover9001("move 2 from 2 to 1");
        sut.ProcessInstructionForCrateMover9001("move 1 from 1 to 2");

        sut.TopOfStacks().Should().Be("MCD");
    }
}
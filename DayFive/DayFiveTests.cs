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
    public void it_can_process_a_moves()
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
}

public class SupplyStacks
{
    private readonly Stack<string>[] stacks;

    public SupplyStacks(string[] initialStacks)
    {
        stacks = new Stack<string>[initialStacks.Length];
        int count = 0;
        initialStacks.ForEach(initial =>
        {
            var stack = new Stack<string>();
            initial.ToCharArray().Reverse().ForEach(item => stack.Push(item.ToString()));
            stacks[count++] = stack;
        });
    }

    public void ProcessInstructionForCrateMover9000(string craneInstruction)
    {
        var tokens = craneInstruction.Split(" ");
        var numberToMove = int.Parse(tokens[1]);
        var fromStack = int.Parse(tokens[3]) - 1;
        var toStack = int.Parse(tokens[5]) - 1;

        Enumerable.Range(1, numberToMove).ForEach(x => stacks[toStack].Push(stacks[fromStack].Pop()));
    }

    public IEnumerable<string> State()
    {
        return stacks.Select(stack => stack.Aggregate(String.Empty, (current, item) => current + item));
    }

    public string TopOfStacks()
    {
        return State().Select(stack => stack[..1]).Aggregate(String.Empty, (current, item) => current + item);
    }

}
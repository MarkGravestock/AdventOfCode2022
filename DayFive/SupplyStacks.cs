namespace DaySix;

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

    public void ProcessInstructionForCrateMover9000(string craneInstructionText)
    {
        var craneInstruction = CraneInstruction.Parse(craneInstructionText);

        Enumerable.Range(1, craneInstruction.numberToMove)
            .ForEach(x => stacks[craneInstruction.toStack].Push(stacks[craneInstruction.fromStack].Pop()));
    }

    public IEnumerable<string> State()
    {
        return stacks.Select(stack => stack.Aggregate(String.Empty, (current, item) => current + item));
    }

    public string TopOfStacks()
    {
        return State().Select(stack => stack[..1]).Aggregate(String.Empty, (current, item) => current + item);
    }

    public void ProcessInstructionForCrateMover9001(string craneInstructionText)
    {
        var craneInstruction = CraneInstruction.Parse(craneInstructionText);

        Stack<string> tempStack = new Stack<string>();

        Enumerable.Range(1, craneInstruction.numberToMove).ForEach(x => tempStack.Push(stacks[craneInstruction.fromStack].Pop()));
        Enumerable.Range(1, craneInstruction.numberToMove).ForEach(x => stacks[craneInstruction.toStack].Push(tempStack.Pop()));
    }
}

public record CraneInstruction(int numberToMove, int fromStack, int toStack)
{
    public static CraneInstruction Parse(string craneInstructionText)
    {
        var tokens = craneInstructionText.Split(" ");
        var numberToMove = int.Parse(tokens[1]);
        var fromStack = int.Parse(tokens[3]) - 1;
        var toStack = int.Parse(tokens[5]) - 1;

        return new CraneInstruction(numberToMove, fromStack, toStack);
    }
}

using FluentAssertions;

namespace DayEleven;

public class DayElevenTests
{
    [Fact]
    public void monkeys_can_process_their_items()
    {
        Monkey[] monkeys = new Monkey[4];

        monkeys[0] = new Monkey
        (
            items: new List<int> {79, 98},
            operation: old => old * 19,
            test: 23
        );

        monkeys[1] = new Monkey(
            items: new List<int> {54, 65, 75, 74},
            operation: old => old + 6,
            test: 19
        );

        monkeys[2] = new Monkey(
            items: new List<int> {79, 60, 97},
            operation: old => old * old,
            test: 13
        );

        monkeys[3] = new Monkey(
            items: new List<int> {74},
            operation: old => old + 3,
            test: 17
        );

        monkeys[0].IfTrue(monkeys[2]);
        monkeys[0].IfFalse(monkeys[3]);

        monkeys[1].IfTrue(monkeys[2]);
        monkeys[1].IfFalse(monkeys[0]);

        monkeys[2].IfTrue(monkeys[1]);
        monkeys[2].IfFalse(monkeys[3]);

        monkeys[3].IfTrue(monkeys[0]);
        monkeys[3].IfFalse(monkeys[1]);

        monkeys[0].TakeTurn();

        monkeys[0].WorryLevels().Should().BeEquivalentTo(new List<int>());
        monkeys[3].WorryLevels().Should().BeEquivalentTo(new List<int> {74, 500, 620});

        monkeys[1].TakeTurn();
        monkeys[2].TakeTurn();
        monkeys[3].TakeTurn();

        monkeys[0].WorryLevels().Should().BeEquivalentTo(new List<int> {20, 23, 27, 26});
        monkeys[1].WorryLevels().Should().BeEquivalentTo(new List<int> {2080, 25, 167, 207, 401, 1046});
        monkeys[2].WorryLevels().Should().BeEquivalentTo(new List<int>());
        monkeys[3].WorryLevels().Should().BeEquivalentTo(new List<int>());
    }

    [Fact]
    public void monkey_zero_can_process_their_items()
    {
        Monkey[] monkeys = new Monkey[4];

        monkeys[0] = new Monkey
        (
            items: new List<int> {79, 98},
            operation: old => old * 19,
            test: 23
        );

        monkeys[1] = new Monkey(
            items: new List<int> {54, 65, 75, 74},
            operation: old => old + 6,
            test: 19
        );

        monkeys[2] = new Monkey(
            items: new List<int> {79, 60, 97},
            operation: old => old * old,
            test: 13
        );

        monkeys[3] = new Monkey(
            items: new List<int> {74},
            operation: old => old + 3,
            test: 17
        );

        monkeys[0].IfTrue(monkeys[2]);
        monkeys[0].IfFalse(monkeys[3]);

        monkeys[1].IfTrue(monkeys[2]);
        monkeys[1].IfFalse(monkeys[0]);

        monkeys[2].IfTrue(monkeys[1]);
        monkeys[2].IfFalse(monkeys[3]);

        monkeys[3].IfTrue(monkeys[0]);
        monkeys[3].IfFalse(monkeys[1]);

        var sut = new MonkeyInTheMiddle(monkeys);

        sut.PlayRounds();

        sut.WorryLevels().Should().BeEquivalentTo(new List<List<int>>() {new() {10, 12, 14, 26, 34},
                                                    new() {245, 93, 53, 199, 115},
                                                    new(),
                                                    new()});


        sut.TotalInspections().Should().BeEquivalentTo(new List<int> {101, 95, 7, 105});
        sut.MonkeyBusiness().Should().Be(10605);

    }
}

public class MonkeyInTheMiddle
{
    private Monkey[] monkeys;

    public MonkeyInTheMiddle(IEnumerable<Monkey> monkeys)
    {
        this.monkeys = monkeys.ToArray();
    }

    public void PlayRounds()
    {
        Enumerable.Range(1, 20).ForEach(round => monkeys.ForEach(monkey => monkey.TakeTurn()));
    }

    public IEnumerable<int> TotalInspections()
    {
        return monkeys.Select(monkey => monkey.TotalInspections());
    }

    public IEnumerable<IEnumerable<int>> WorryLevels()
    {
        return monkeys.Select(monkey => monkey.WorryLevels());
    }

    public int MonkeyBusiness()
    {
        var top2 = TotalInspections().OrderDescending().Take(2).ToArray();
        return top2[0] * top2[1];
    }
}

public class Monkey
{
    private readonly List<int> items;
    private readonly Func<int, int> operation;
    private readonly int test;
    private Monkey ifTrue;
    private Monkey ifFalse;

    private int inspectionCount;

    public Monkey(List<int> items, Func<int, int> operation, int test)
    {
        this.items = items;
        this.operation = operation;
        this.test = test;
    }

    public void IfTrue(Monkey monkey)
    {
        ifTrue = monkey;
    }

    public void IfFalse(Monkey monkey)
    {
        ifFalse = monkey;
    }

    public void TakeTurn()
    {
        items.Select(x => (x, operation(x)))
            .Select(w => (w.Item1, w.Item2 / 3))
            .ForEach(z =>
            {
                var thrownTo = (z.Item2 % test == 0) ? ifTrue : ifFalse;
                thrownTo.Throw(z.Item2);
            });

        inspectionCount += items.Count();

        items.Clear();
    }

    private void Throw(int item)
    {
        items.Add(item);
    }

    public IEnumerable<int> WorryLevels()
    {
        return items;
    }

    public int TotalInspections()
    {
        return inspectionCount;
    }
}

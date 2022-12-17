using FluentAssertions;

namespace DayEleven;

public class DayElevenActualTest
{
    [Fact]
    public void monkeys_can_play_rounds()
    {
        Monkey[] monkeys = new Monkey[8];

        /*
        Monkey 0:
        Starting items: 57, 58
        Operation: new = old * 19
        Test: divisible by 7
        If true: throw to monkey 2
        If false: throw to monkey 3
        */
        monkeys[0] = new Monkey
        (
            items: new List<int> {57, 58},
            operation: old => old * 19,
            test: 7
        );

        /*
        Monkey 1:
        Starting items: 66, 52, 59, 79, 94, 73
        Operation: new = old + 1
        Test: divisible by 19
        If true: throw to monkey 4
        If false: throw to monkey 6
        */
        monkeys[1] = new Monkey
        (
            items: new List<int> {66, 52, 59, 79, 94, 73},
            operation: old => old + 1,
            test: 19
        );

        /*Monkey 2:
        Starting items: 80
        Operation: new = old + 6
        Test: divisible by 5
        If true: throw to monkey 7
        If false: throw to monkey 5*/

        monkeys[2] = new Monkey
        (
            items: new List<int> {80},
            operation: old => old + 6,
            test: 5
        );

        /*
        Monkey 3:
        Starting items: 82, 81, 68, 66, 71, 83, 75, 97
        Operation: new = old + 5
        Test: divisible by 11
        If true: throw to monkey 5
        If false: throw to monkey 2
        */

        monkeys[3] = new Monkey
        (
            items: new List<int> {82, 81, 68, 66, 71, 83, 75, 97},
            operation: old => old + 5,
            test: 11
        );

        /*
        Monkey 4:
        Starting items: 55, 52, 67, 70, 69, 94, 90
        Operation: new = old * old
        Test: divisible by 17
        If true: throw to monkey 0
        If false: throw to monkey 3
        */

        monkeys[4] = new Monkey
        (
            items: new List<int> {55, 52, 67, 70, 69, 94, 90},
            operation: old => old * old,
            test: 17
        );
        /*
        Monkey 5:
        Starting items: 69, 85, 89, 91
        Operation: new = old + 7
        Test: divisible by 13
        If true: throw to monkey 1
        If false: throw to monkey 7
        */

        monkeys[5] = new Monkey
        (
            items: new List<int> {69, 85, 89, 91},
            operation: old => old + 7,
            test: 13
        );
        /*
        Monkey 6:
        Starting items: 75, 53, 73, 52, 75
        Operation: new = old * 7
        Test: divisible by 2
        If true: throw to monkey 0
        If false: throw to monkey 4
        */

        monkeys[6] = new Monkey
        (
            items: new List<int> {75, 53, 73, 52, 75},
            operation: old => old * 7,
            test: 2
        );

        /*
        Monkey 7:
        Starting items: 94, 60, 79
        Operation: new = old + 2
        Test: divisible by 3
        If true: throw to monkey 1
        If false: throw to monkey 6
        */

        monkeys[7] = new Monkey
        (
            items: new List<int> {94, 60, 79},
            operation: old => old + 2,
            test: 3
        );

        //0
        monkeys[0].IfTrue(monkeys[2]);
        monkeys[0].IfFalse(monkeys[3]);

        //1
        monkeys[1].IfTrue(monkeys[4]);
        monkeys[1].IfFalse(monkeys[6]);

        ///2
        monkeys[2].IfTrue(monkeys[7]);
        monkeys[2].IfFalse(monkeys[5]);

        // 3
        monkeys[3].IfTrue(monkeys[5]);
        monkeys[3].IfFalse(monkeys[2]);

        //4
        monkeys[4].IfTrue(monkeys[0]);
        monkeys[4].IfFalse(monkeys[3]);

        // 5
        monkeys[5].IfTrue(monkeys[1]);
        monkeys[5].IfFalse(monkeys[7]);

        //6
        monkeys[6].IfTrue(monkeys[0]);
        monkeys[6].IfFalse(monkeys[4]);

        //7
        monkeys[7].IfTrue(monkeys[1]);
        monkeys[7].IfFalse(monkeys[6]);

        var sut = new MonkeyInTheMiddle(monkeys);

        sut.PlayRounds();

        sut.MonkeyBusiness().Should().Be(50830);

    }

}
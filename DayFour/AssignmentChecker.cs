namespace DayFour;

public class AssignmentChecker
{
    public bool DoesSectionCompletelyContainTheOther(string sectionAssignments)
    {
        var assignments = sectionAssignments.Split(",");
        var firstRange = CreateRangeFromAssignment(assignments[0]);
        var secondRange = CreateRangeFromAssignment(assignments[1]);;

        return secondRange.Includes(firstRange) || firstRange.Includes(secondRange);
    }

    private static InclusiveRange CreateRangeFromAssignment(string assignment)
    {
        var firstAssignment = assignment.Split("-");
        var firstRange = new InclusiveRange(int.Parse(firstAssignment[0]), int.Parse(firstAssignment[1]));
        return firstRange;
    }

    public int TotalSectionsCompletelyContainingTheOther(IEnumerable<string> sectionAssignments)
    {
        return sectionAssignments.Select(DoesSectionCompletelyContainTheOther).Count(x => x);
    }
}
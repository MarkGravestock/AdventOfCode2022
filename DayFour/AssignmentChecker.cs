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

    private static InclusiveRange CreateRangeFromAssignment(string assignmentDefinition)
    {
        var assignment = assignmentDefinition.Split("-");
        return new InclusiveRange(int.Parse(assignment[0]), int.Parse(assignment[1]));
    }

    public int TotalSectionsCompletelyContainingTheOther(IEnumerable<string> sectionAssignments)
    {
        return sectionAssignments.Select(DoesSectionCompletelyContainTheOther).Count(x => x);
    }

    public bool DoesSectionOverlapTheOther(string sectionAssignments)
    {
        var assignments = sectionAssignments.Split(",");
        var firstRange = CreateRangeFromAssignment(assignments[0]);
        var secondRange = CreateRangeFromAssignment(assignments[1]);;

        return secondRange.Overlaps(firstRange) || firstRange.Overlaps(secondRange);
    }

    public int TotalSectionsOverlappingTheOther(IEnumerable<string> sectionAssignments)
    {
        return sectionAssignments.Select(DoesSectionOverlapTheOther).Count(x => x);
    }

}
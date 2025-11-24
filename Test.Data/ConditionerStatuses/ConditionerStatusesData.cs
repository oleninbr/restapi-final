using Domain.ConditionerStatuses;

namespace Test.Data.ConditionerStatuses;

public static class ConditionerStatusesData
{
    public static ConditionerStatus FirstTestStatus()
        => ConditionerStatus.New(ConditionerStatusId.New(), "Active");

    public static ConditionerStatus SecondTestStatus()
        => ConditionerStatus.New(ConditionerStatusId.New(), "Inactive");
}

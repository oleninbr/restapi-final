using Domain.ConditionerStatuses;

namespace Tests.Data.ConditionerStatuses;

public static class ConditionerStatusesData
{
    public static ConditionerStatus FirstTestStatus()
        => ConditionerStatus.New(ConditionerStatusId.New(), "Active");

    public static ConditionerStatus SecondTestStatus()
        => ConditionerStatus.New(ConditionerStatusId.New(), "Inactive");
}

using Domain.WorkOrderPriorities;

namespace Tests.Data.WorkOrderPriorities;

public static class WorkOrderPrioritiesData
{
    public static WorkOrderPriority FirstTestPriority()
        => WorkOrderPriority.New(WorkOrderPriorityId.New(), "High");

    public static WorkOrderPriority SecondTestPriority()
        => WorkOrderPriority.New(WorkOrderPriorityId.New(), "Low");
}

using Domain.WorkOrderStatuses;

namespace Tests.Data.WorkOrderStatuses;

public static class WorkOrderStatusesData
{
    public static WorkOrderStatus FirstTestStatus()
        => WorkOrderStatus.New(WorkOrderStatusId.New(), "Pending");

    public static WorkOrderStatus SecondTestStatus()
        => WorkOrderStatus.New(WorkOrderStatusId.New(), "Completed");
}

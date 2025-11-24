using Domain.Conditioners;
using Domain.WorkOrders;
using Domain.WorkOrderStatuses;
using Domain.WorkOrderPriorities;

namespace Tests.Data.WorkOrders;

public static class WorkOrdersData
{
    public static WorkOrder FirstTestWorkOrder()
        => WorkOrder.New(
            WorkOrderId.New(),
            "WO-1001",
            new ConditionerId(Guid.NewGuid()),
            "Fix Compressor",
            "Replace damaged compressor and test cooling.",
            new WorkOrderPriorityId(Guid.NewGuid()),
            new WorkOrderStatusId(Guid.NewGuid()),
            DateTime.UtcNow.AddDays(3)
        );

    public static WorkOrder SecondTestWorkOrder()
        => WorkOrder.New(
            WorkOrderId.New(),
            "WO-1002",
            new ConditionerId(Guid.NewGuid()),
            "Clean Filters",
            "Routine filter cleaning for maintenance.",
            new WorkOrderPriorityId(Guid.NewGuid()),
            new WorkOrderStatusId(Guid.NewGuid()),
            DateTime.UtcNow.AddDays(7)
        );
}

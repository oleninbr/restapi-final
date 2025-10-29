using Domain.WorkOrder;
using System;

namespace WebAPI.Dtos;

public record WorkOrderDto(Guid Id, string WorkOrderNumber, string Title, string Description, DateTime ScheduledDate, Guid ConditionerId, Guid PriorityId, Guid StatusId, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static WorkOrderDto FromDomainModel(WorkOrder workOrder)
        => new(workOrder.Id, workOrder.WorkOrderNumber, workOrder.Title, workOrder.Description, workOrder.ScheduledDate, workOrder.ConditionerId, workOrder.PriorityId, workOrder.StatusId, workOrder.CreatedAt, workOrder.UpdatedAt);
}

public record CreateWorkOrderDto(string WorkOrderNumber, string Title, string Description, DateTime ScheduledDate, Guid ConditionerId, Guid PriorityId, Guid StatusId);

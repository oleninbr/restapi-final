using Domain.WorkOrders;

namespace WebAPI.Dtos;

public record WorkOrderDto(
    Guid Id,
    string WorkOrderNumber,
    string Title,
    string Description,
    DateTime ScheduledDate,
    Guid ConditionerId,
    Guid PriorityId,
    Guid StatusId,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static WorkOrderDto FromDomainModel(WorkOrder workOrder)
        => new(
            workOrder.Id.Value,
            workOrder.WorkOrderNumber,
            workOrder.Title,
            workOrder.Description,
            workOrder.ScheduledDate,
            workOrder.ConditionerId.Value,
            workOrder.PriorityId.Value,
            workOrder.StatusId.Value,
            workOrder.CreatedAt,
            workOrder.UpdatedAt);
}

public record CreateWorkOrderDto(
    string WorkOrderNumber,
    string Title,
    string Description,
    DateTime ScheduledDate,
    Guid ConditionerId,
    Guid PriorityId,
    Guid StatusId);


public record UpdateWorkOrderDto(
    Guid Id,
    string Title,
    string Description,
    Guid PriorityId,
    Guid StatusId,
    DateTime ScheduledDate
);


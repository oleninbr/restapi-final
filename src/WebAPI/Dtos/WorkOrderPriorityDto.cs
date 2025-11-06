using Domain.WorkOrderPriorities;

namespace WebAPI.Dtos;

public record WorkOrderPriorityDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static WorkOrderPriorityDto FromDomainModel(WorkOrderPriority priority)
        => new(
            priority.Id.Value,
            priority.Name,
            priority.CreatedAt,
            priority.UpdatedAt);
}

public record CreateWorkOrderPriorityDto(string Name);

public record UpdateWorkOrderPriorityDto(
    Guid Id,
    string Name);
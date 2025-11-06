using Domain.WorkOrderStatuses;

namespace WebAPI.Dtos;

public record WorkOrderStatusDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static WorkOrderStatusDto FromDomainModel(WorkOrderStatus status)
        => new(
            status.Id.Value,
            status.Name,
            status.CreatedAt,
            status.UpdatedAt);
}

public record CreateWorkOrderStatusDto(string Name);

public record UpdateWorkOrderStatusDto(
    Guid Id,
    string Name);
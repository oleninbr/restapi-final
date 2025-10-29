using Domain.WorkOrderStatuses;
using System;

namespace WebAPI.Dtos;

public record WorkOrderStatusDto(Guid Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static WorkOrderStatusDto FromDomainModel(WorkOrderStatus status)
        => new(status.Id, status.Name, status.CreatedAt, status.UpdatedAt);
}

public record CreateWorkOrderStatusDto(string Name);

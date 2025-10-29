using Domain.WorkOrderPriorities;
using System;

namespace WebAPI.Dtos;

public record WorkOrderPriorityDto(Guid Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static WorkOrderPriorityDto FromDomainModel(WorkOrderPriority priority)
        => new(priority.Id, priority.Name, priority.CreatedAt, priority.UpdatedAt);
}

public record CreateWorkOrderPriorityDto(string Name);

using Domain.ConditionerStatus;
using System;

namespace WebAPI.Dtos;

public record ConditionerStatusDto(Guid Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static ConditionerStatusDto FromDomainModel(ConditionerStatus status)
        => new(status.Id, status.Name, status.CreatedAt, status.UpdatedAt);
}

public record CreateConditionerStatusDto(string Name);

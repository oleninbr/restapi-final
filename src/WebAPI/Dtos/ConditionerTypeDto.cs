using Domain.ConditionerType;
using System;

namespace WebAPI.Dtos;

public record ConditionerTypeDto(Guid Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static ConditionerTypeDto FromDomainModel(ConditionerType type)
        => new(type.Id, type.Name, type.CreatedAt, type.UpdatedAt);
}

public record CreateConditionerTypeDto(string Name);

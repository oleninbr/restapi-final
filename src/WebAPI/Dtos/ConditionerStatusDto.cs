using Domain.ConditionerStatuses;

namespace WebAPI.Dtos;

public record ConditionerStatusDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static ConditionerStatusDto FromDomainModel(ConditionerStatus status)
        => new(
            status.Id.Value,
            status.Name,
            status.CreatedAt,
            status.UpdatedAt);
}

public record CreateConditionerStatusDto(string Name);

public record UpdateConditionerStatusDto(
    Guid Id,
    string Name);
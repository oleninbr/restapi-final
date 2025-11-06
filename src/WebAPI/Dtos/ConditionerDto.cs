using Domain.Conditioners;

namespace WebAPI.Dtos;

public record ConditionerDto(
    Guid Id,
    string Name,
    string Model,
    string SerialNumber,
    string Location,
    DateTime InstallationDate,
    Guid StatusId,
    Guid TypeId,
    Guid ManufacturerId,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static ConditionerDto FromDomainModel(Conditioner conditioner)
        => new(
            conditioner.Id.Value,
            conditioner.Name,
            conditioner.Model,
            conditioner.SerialNumber,
            conditioner.Location,
            conditioner.InstallationDate,
            conditioner.StatusId,
            conditioner.TypeId,
            conditioner.ManufacturerId,
            conditioner.CreatedAt,
            conditioner.UpdatedAt);
}

public record CreateConditionerDto(
    string Name,
    string Model,
    string SerialNumber,
    string Location,
    DateTime InstallationDate,
    Guid StatusId,
    Guid TypeId,
    Guid ManufacturerId);

public record UpdateConditionerDto(
    Guid Id,
    string Name,
    string Model,
    string SerialNumber,
    string Location,
    DateTime InstallationDate,
    Guid StatusId,
    Guid TypeId,
    Guid ManufacturerId);
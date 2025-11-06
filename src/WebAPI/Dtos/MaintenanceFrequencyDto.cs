using Domain.MaintenanceFrequencies;

namespace WebAPI.Dtos;

public record MaintenanceFrequencyDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static MaintenanceFrequencyDto FromDomainModel(MaintenanceFrequency freq)
        => new(
            freq.Id.Value,
            freq.Name,
            freq.CreatedAt,
            freq.UpdatedAt);
}

public record CreateMaintenanceFrequencyDto(string Name);

public record UpdateMaintenanceFrequencyDto(
    Guid Id,
    string Name);
using Domain.Entities;
using System;

namespace WebAPI.Dtos;

public record MaintenanceFrequencyDto(Guid Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static MaintenanceFrequencyDto FromDomainModel(MaintenanceFrequency freq)
        => new(freq.Id, freq.Name, freq.CreatedAt, freq.UpdatedAt);
}

public record CreateMaintenanceFrequencyDto(string Name);

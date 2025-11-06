using Domain.Manufacturers;

namespace WebAPI.Dtos;

public record ManufacturerDto(
    Guid Id,
    string Name,
    string Country,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static ManufacturerDto FromDomainModel(Manufacturer manufacturer)
        => new(
            manufacturer.Id.Value,
            manufacturer.Name,
            manufacturer.Country,
            manufacturer.CreatedAt,
            manufacturer.UpdatedAt);
}

public record CreateManufacturerDto(string Name, string Country);

public record UpdateManufacturerDto(
    Guid Id,
    string Name,
    string Country);

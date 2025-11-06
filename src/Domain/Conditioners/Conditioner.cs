using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.Manufacturers;

namespace Domain.Conditioners;

public class Conditioner
{
    public ConditionerId Id { get; }
    public string Name { get; private set; }
    public string Model { get; private set; }
    public string SerialNumber { get; private set; }
    public string Location { get; private set; }
    public DateTime InstallationDate { get; private set; }
    public Guid StatusId { get; private set; }
    public Guid TypeId { get; private set; }
    public Guid ManufacturerId { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    // 🔹 Навігаційні властивості
    public ConditionerStatus? Status { get; private set; }
    public ConditionerType? Type { get; private set; }
    public Manufacturer? Manufacturer { get; private set; }

    private Conditioner(
        ConditionerId id,
        string name,
        string model,
        string serialNumber,
        string location,
        DateTime installationDate,
        Guid statusId,
        Guid typeId,
        Guid manufacturerId,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Model = model;
        SerialNumber = serialNumber;
        Location = location;
        InstallationDate = installationDate;
        StatusId = statusId;
        TypeId = typeId;
        ManufacturerId = manufacturerId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Conditioner New(
        ConditionerId id,
        string name,
        string model,
        string serialNumber,
        string location,
        DateTime installationDate,
        Guid statusId,
        Guid typeId,
        Guid manufacturerId)
    {
        return new Conditioner(
            id, name, model, serialNumber, location,
            installationDate, statusId, typeId, manufacturerId,
            DateTime.UtcNow, null);
    }

    public void UpdateDetails(
        string name,
        string model,
        string serialNumber,
        string location,
        DateTime installationDate,
        Guid statusId,
        Guid typeId,
        Guid manufacturerId)
    {
        Name = name;
        Model = model;
        SerialNumber = serialNumber;
        Location = location;
        InstallationDate = installationDate;
        StatusId = statusId;
        TypeId = typeId;
        ManufacturerId = manufacturerId;
        UpdatedAt = DateTime.UtcNow;
    }
}

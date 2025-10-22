namespace Domain.Entities;

public class Conditioner
{
    public Guid Id { get; }
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

    private Conditioner(
        Guid id,
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
        Guid id,
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

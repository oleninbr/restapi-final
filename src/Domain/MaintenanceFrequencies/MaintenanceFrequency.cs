namespace Domain.MaintenanceFrequencies;

public class MaintenanceFrequency
{
    public MaintenanceFrequencyId Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private MaintenanceFrequency(MaintenanceFrequencyId id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static MaintenanceFrequency New(MaintenanceFrequencyId id, string name)
        => new(id, name, DateTime.UtcNow, null);

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

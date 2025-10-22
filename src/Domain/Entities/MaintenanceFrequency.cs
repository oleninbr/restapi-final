namespace Domain.Entities;

public class MaintenanceFrequency
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private MaintenanceFrequency(Guid id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static MaintenanceFrequency New(Guid id, string name)
    {
        return new MaintenanceFrequency(id, name, DateTime.UtcNow, null);
    }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

namespace Domain.ConditionerStatus;

public class ConditionerStatus
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private ConditionerStatus(Guid id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ConditionerStatus New(Guid id, string name)
    {
        return new ConditionerStatus(id, name, DateTime.UtcNow, null);
    }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

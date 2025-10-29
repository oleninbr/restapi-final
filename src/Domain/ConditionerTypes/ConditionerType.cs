namespace Domain.ConditionerType;

public class ConditionerType
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private ConditionerType(Guid id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ConditionerType New(Guid id, string name)
    {
        return new ConditionerType(id, name, DateTime.UtcNow, null);
    }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

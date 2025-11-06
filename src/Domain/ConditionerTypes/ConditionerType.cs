namespace Domain.ConditionerTypes;

public class ConditionerType
{
    public ConditionerTypeId Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private ConditionerType(ConditionerTypeId id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ConditionerType New(ConditionerTypeId id, string name)
        => new(id, name, DateTime.UtcNow, null);

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

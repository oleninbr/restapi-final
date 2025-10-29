namespace Domain.WorkOrderPriorities;

public class WorkOrderPriority
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private WorkOrderPriority(Guid id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static WorkOrderPriority New(Guid id, string name)
    {
        return new WorkOrderPriority(id, name, DateTime.UtcNow, null);
    }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

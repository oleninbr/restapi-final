namespace Domain.WorkOrderPriorities;

public class WorkOrderPriority
{
    public WorkOrderPriorityId Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private WorkOrderPriority(WorkOrderPriorityId id, string name, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static WorkOrderPriority New(WorkOrderPriorityId id, string name)
        => new(id, name, DateTime.UtcNow, null);

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}

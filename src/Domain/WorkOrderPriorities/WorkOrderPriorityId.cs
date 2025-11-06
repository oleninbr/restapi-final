namespace Domain.WorkOrderPriorities;

public readonly record struct WorkOrderPriorityId(Guid Value)
{
    public static WorkOrderPriorityId Empty() => new(Guid.Empty);
    public static WorkOrderPriorityId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

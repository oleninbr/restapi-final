namespace Domain.WorkOrderStatuses;

public readonly record struct WorkOrderStatusId(Guid Value)
{
    public static WorkOrderStatusId Empty() => new(Guid.Empty);
    public static WorkOrderStatusId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

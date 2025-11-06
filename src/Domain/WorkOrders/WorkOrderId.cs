namespace Domain.WorkOrders;

public readonly record struct WorkOrderId(Guid Value)
{
    public static WorkOrderId Empty() => new(Guid.Empty);
    public static WorkOrderId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

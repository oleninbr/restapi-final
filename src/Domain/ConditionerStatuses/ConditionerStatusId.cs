namespace Domain.ConditionerStatuses;

public readonly record struct ConditionerStatusId(Guid Value)
{
    public static ConditionerStatusId Empty() => new(Guid.Empty);
    public static ConditionerStatusId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

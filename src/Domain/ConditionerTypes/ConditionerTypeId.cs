namespace Domain.ConditionerTypes;

public readonly record struct ConditionerTypeId(Guid Value)
{
    public static ConditionerTypeId Empty() => new(Guid.Empty);
    public static ConditionerTypeId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

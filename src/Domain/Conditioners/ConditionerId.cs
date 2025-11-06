namespace Domain.Conditioners;

public readonly record struct ConditionerId(Guid Value)
{
    public static ConditionerId Empty() => new(Guid.Empty);
    public static ConditionerId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

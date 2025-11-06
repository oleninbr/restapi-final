namespace Domain.MaintenanceFrequencies;

public readonly record struct MaintenanceFrequencyId(Guid Value)
{
    public static MaintenanceFrequencyId Empty() => new(Guid.Empty);
    public static MaintenanceFrequencyId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

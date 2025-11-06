namespace Domain.MaintenanceSchedules;

public readonly record struct MaintenanceScheduleId(Guid Value)
{
    public static MaintenanceScheduleId Empty() => new(Guid.Empty);
    public static MaintenanceScheduleId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

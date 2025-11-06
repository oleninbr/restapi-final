using Domain.MaintenanceSchedules;

namespace Domain.Conditioners;

public class ConditionerMaintenanceSchedule
{
    public ConditionerId ConditionerId { get; }
    public Conditioner? Conditioner { get; private set; }

    public MaintenanceScheduleId MaintenanceScheduleId { get; }
    public MaintenanceSchedule? MaintenanceSchedule { get; private set; }

    private ConditionerMaintenanceSchedule(ConditionerId conditionerId, MaintenanceScheduleId maintenanceScheduleId)
        => (ConditionerId, MaintenanceScheduleId) = (conditionerId, maintenanceScheduleId);

    public static ConditionerMaintenanceSchedule New(ConditionerId conditionerId, MaintenanceScheduleId maintenanceScheduleId)
        => new(conditionerId, maintenanceScheduleId);
}

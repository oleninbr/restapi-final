using Domain.Conditioners;
using Domain.MaintenanceSchedules;

namespace Tests.Data.ConditionerMaintenanceSchedules;

public static class ConditionerMaintenanceSchedulesData
{
    public static ConditionerMaintenanceSchedule Link(ConditionerId conditionerId, MaintenanceScheduleId scheduleId)
        => ConditionerMaintenanceSchedule.New(conditionerId, scheduleId);
}

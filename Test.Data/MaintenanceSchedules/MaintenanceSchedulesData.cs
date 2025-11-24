using Domain.MaintenanceSchedules;
using Domain.MaintenanceFrequencies;
using Domain.Conditioners;

namespace Test.Data.MaintenanceSchedules;

public static class MaintenanceSchedulesData
{
    public static MaintenanceSchedule FirstTestSchedule()
        => MaintenanceSchedule.New(
            MaintenanceScheduleId.New(),
            ConditionerId.New(),
            "Filter Replacement",
            "Replace filter every 3 months",
            new MaintenanceFrequencyId(Guid.NewGuid()),
            DateTime.UtcNow.AddMonths(3),
            true);

    public static MaintenanceSchedule SecondTestSchedule()
        => MaintenanceSchedule.New(
            MaintenanceScheduleId.New(),
            ConditionerId.New(),
            "Compressor Check",
            "Check compressor performance every 6 months",
            new MaintenanceFrequencyId(Guid.NewGuid()),
            DateTime.UtcNow.AddMonths(6),
            true);
}

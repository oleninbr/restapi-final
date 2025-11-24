using Domain.MaintenanceFrequencies;

namespace Test.Data.MaintenanceFrequencies;

public static class MaintenanceFrequenciesData
{
    public static MaintenanceFrequency FirstTestFrequency()
        => MaintenanceFrequency.New(MaintenanceFrequencyId.New(), "Weekly");

    public static MaintenanceFrequency SecondTestFrequency()
        => MaintenanceFrequency.New(MaintenanceFrequencyId.New(), "Monthly");
}

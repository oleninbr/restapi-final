using Domain.MaintenanceSchedules;

namespace Application.MaintenanceSchedules.Exceptions;

public abstract class MaintenanceScheduleException(MaintenanceScheduleId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public MaintenanceScheduleId ScheduleId { get; } = id;
}

public class MaintenanceScheduleNotFoundException(MaintenanceScheduleId id)
    : MaintenanceScheduleException(id, $"Maintenance schedule not found under id {id}");

public class MaintenanceScheduleAlreadyExistException(MaintenanceScheduleId id)
    : MaintenanceScheduleException(id, $"Maintenance schedule already exists under id {id}");

public class MaintenanceScheduleConditionerNotFoundException(MaintenanceScheduleId id)
    : MaintenanceScheduleException(id, $"Conditioner not found for maintenance schedule {id}");

public class MaintenanceScheduleFrequencyNotFoundException(MaintenanceScheduleId id)
    : MaintenanceScheduleException(id, $"Maintenance frequency not found for maintenance schedule {id}");

public class UnhandledMaintenanceScheduleException(MaintenanceScheduleId id, Exception? innerException = null)
    : MaintenanceScheduleException(id, "Unexpected error occurred", innerException);

using Domain.MaintenanceFrequencies;

namespace Application.MaintenanceFrequencies.Exceptions;

public abstract class MaintenanceFrequencyException(MaintenanceFrequencyId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public MaintenanceFrequencyId MaintenanceFrequencyId { get; } = id;
}

public class MaintenanceFrequencyAlreadyExistException(MaintenanceFrequencyId id)
    : MaintenanceFrequencyException(id, $"Maintenance frequency already exists under id {id}");

public class MaintenanceFrequencyNotFoundException(MaintenanceFrequencyId id)
    : MaintenanceFrequencyException(id, $"Maintenance frequency not found under id {id}");

public class UnhandledMaintenanceFrequencyException(MaintenanceFrequencyId id, Exception? innerException = null)
    : MaintenanceFrequencyException(id, "Unexpected error occurred", innerException);

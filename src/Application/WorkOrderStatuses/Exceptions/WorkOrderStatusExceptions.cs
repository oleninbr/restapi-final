using Domain.WorkOrderStatuses;

namespace Application.WorkOrderStatuses.Exceptions;

public abstract class WorkOrderStatusException(WorkOrderStatusId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public WorkOrderStatusId WorkOrderStatusId { get; } = id;
}

public class WorkOrderStatusAlreadyExistException(WorkOrderStatusId id)
    : WorkOrderStatusException(id, $"Work order status already exists under id {id}");

public class WorkOrderStatusNotFoundException(WorkOrderStatusId id)
    : WorkOrderStatusException(id, $"Work order status not found under id {id}");

public class UnhandledWorkOrderStatusException(WorkOrderStatusId id, Exception? innerException = null)
    : WorkOrderStatusException(id, "Unexpected error occurred", innerException);

using Domain.WorkOrderPriorities;

namespace Application.WorkOrderPriorities.Exceptions;

public abstract class WorkOrderPriorityException(WorkOrderPriorityId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public WorkOrderPriorityId WorkOrderPriorityId { get; } = id;
}

public class WorkOrderPriorityAlreadyExistException(WorkOrderPriorityId id)
    : WorkOrderPriorityException(id, $"Work order priority already exists under id {id}");

public class WorkOrderPriorityNotFoundException(WorkOrderPriorityId id)
    : WorkOrderPriorityException(id, $"Work order priority not found under id {id}");

public class UnhandledWorkOrderPriorityException(WorkOrderPriorityId id, Exception? innerException = null)
    : WorkOrderPriorityException(id, "Unexpected error occurred", innerException);

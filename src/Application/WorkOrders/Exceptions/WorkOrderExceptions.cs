using Domain.WorkOrders;

namespace Application.WorkOrders.Exceptions;

public abstract class WorkOrderException(WorkOrderId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public WorkOrderId WorkOrderId { get; } = id;
}

public class WorkOrderAlreadyExistException(WorkOrderId id)
    : WorkOrderException(id, $"Work order already exists under id {id}");

public class WorkOrderNotFoundException(WorkOrderId id)
    : WorkOrderException(id, $"Work order not found under id {id}");

public class ConditionerNotFoundForWorkOrderException(WorkOrderId id)
    : WorkOrderException(id, $"Conditioner not found for work order {id}");

public class WorkOrderPriorityNotFoundException(WorkOrderId id)
    : WorkOrderException(id, $"Work order priority not found for work order {id}");

public class WorkOrderStatusNotFoundException(WorkOrderId id)
    : WorkOrderException(id, $"Work order status not found for work order {id}");

public class UnhandledWorkOrderException(WorkOrderId id, Exception? innerException = null)
    : WorkOrderException(id, "Unexpected error occurred", innerException);

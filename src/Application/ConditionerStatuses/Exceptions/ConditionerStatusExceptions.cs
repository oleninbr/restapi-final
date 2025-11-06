using Domain.ConditionerStatuses;

namespace Application.ConditionerStatuses.Exceptions;

public abstract class ConditionerStatusException(ConditionerStatusId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ConditionerStatusId ConditionerStatusId { get; } = id;
}

public class ConditionerStatusAlreadyExistException(ConditionerStatusId id)
    : ConditionerStatusException(id, $"Conditioner status already exists under id {id}");

public class ConditionerStatusNotFoundException(ConditionerStatusId id)
    : ConditionerStatusException(id, $"Conditioner status not found under id {id}");

public class UnhandledConditionerStatusException(ConditionerStatusId id, Exception? innerException = null)
    : ConditionerStatusException(id, "Unexpected error occurred", innerException);

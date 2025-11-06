using Domain.ConditionerTypes;

namespace Application.ConditionerTypes.Exceptions;

public abstract class ConditionerTypeException(ConditionerTypeId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ConditionerTypeId ConditionerTypeId { get; } = id;
}

public class ConditionerTypeAlreadyExistException(ConditionerTypeId id)
    : ConditionerTypeException(id, $"Conditioner type already exists under id {id}");

public class ConditionerTypeNotFoundException(ConditionerTypeId id)
    : ConditionerTypeException(id, $"Conditioner type not found under id {id}");

public class UnhandledConditionerTypeException(ConditionerTypeId id, Exception? innerException = null)
    : ConditionerTypeException(id, "Unexpected error occurred", innerException);

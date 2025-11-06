using Domain.Conditioners;

namespace Application.Conditioners.Exceptions;

public abstract class ConditionerException(ConditionerId conditionerId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ConditionerId ConditionerId { get; } = conditionerId;
}

public class ConditionerAlreadyExistException(ConditionerId conditionerId)
    : ConditionerException(conditionerId, $"Conditioner already exists under id {conditionerId}");

public class ConditionerNotFoundException(ConditionerId conditionerId)
    : ConditionerException(conditionerId, $"Conditioner not found under id {conditionerId}");

public class ConditionerStatusNotFoundException(ConditionerId conditionerId)
    : ConditionerException(conditionerId, $"Conditioner status not found for conditioner {conditionerId}");

public class ConditionerTypeNotFoundException(ConditionerId conditionerId)
    : ConditionerException(conditionerId, $"Conditioner type not found for conditioner {conditionerId}");

public class ManufacturerNotFoundException(ConditionerId conditionerId)
    : ConditionerException(conditionerId, $"Manufacturer not found for conditioner {conditionerId}");

public class UnhandledConditionerException(ConditionerId conditionerId, Exception? innerException = null)
    : ConditionerException(conditionerId, "Unexpected error occurred", innerException);

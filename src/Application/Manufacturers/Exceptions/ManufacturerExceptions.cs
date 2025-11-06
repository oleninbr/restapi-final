using Domain.Manufacturers;

namespace Application.Manufacturers.Exceptions;

public abstract class ManufacturerException(ManufacturerId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ManufacturerId ManufacturerId { get; } = id;
}

public class ManufacturerAlreadyExistException(ManufacturerId id)
    : ManufacturerException(id, $"Manufacturer already exists under id {id}");

public class ManufacturerNotFoundException(ManufacturerId id)
    : ManufacturerException(id, $"Manufacturer not found under id {id}");

public class UnhandledManufacturerException(ManufacturerId id, Exception? innerException = null)
    : ManufacturerException(id, "Unexpected error occurred", innerException);

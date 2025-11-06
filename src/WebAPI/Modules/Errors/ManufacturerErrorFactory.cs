using Application.Manufacturers.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class ManufacturerErrorFactory
{
    public static ObjectResult ToObjectResult(this ManufacturerException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                ManufacturerAlreadyExistException => StatusCodes.Status409Conflict,
                ManufacturerNotFoundException => StatusCodes.Status404NotFound,
                UnhandledManufacturerException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Manufacturer error handler not implemented")
            }
        };
    }
}

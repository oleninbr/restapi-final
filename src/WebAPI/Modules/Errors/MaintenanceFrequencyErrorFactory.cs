using Application.MaintenanceFrequencies.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class MaintenanceFrequencyErrorFactory
{
    public static ObjectResult ToObjectResult(this MaintenanceFrequencyException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                MaintenanceFrequencyAlreadyExistException => StatusCodes.Status409Conflict,
                MaintenanceFrequencyNotFoundException => StatusCodes.Status404NotFound,
                UnhandledMaintenanceFrequencyException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("MaintenanceFrequency error handler not implemented")
            }
        };
    }
}

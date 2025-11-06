using Application.MaintenanceSchedules.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class MaintenanceScheduleErrorFactory
{
    public static ObjectResult ToObjectResult(this MaintenanceScheduleException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                MaintenanceScheduleAlreadyExistException => StatusCodes.Status409Conflict,
                MaintenanceScheduleNotFoundException => StatusCodes.Status404NotFound,
                UnhandledMaintenanceScheduleException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("MaintenanceSchedule error handler not implemented")
            }
        };
    }
}

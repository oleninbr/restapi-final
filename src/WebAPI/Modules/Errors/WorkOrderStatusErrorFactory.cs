using Application.WorkOrderStatuses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class WorkOrderStatusErrorFactory
{
    public static ObjectResult ToObjectResult(this WorkOrderStatusException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                WorkOrderStatusAlreadyExistException => StatusCodes.Status409Conflict,
                WorkOrderStatusNotFoundException => StatusCodes.Status404NotFound,
                UnhandledWorkOrderStatusException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("WorkOrderStatus error handler is not implemented")
            }
        };
    }
}

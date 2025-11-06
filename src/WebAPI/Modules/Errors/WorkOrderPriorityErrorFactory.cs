using Application.WorkOrderPriorities.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class WorkOrderPriorityErrorFactory
{
    public static ObjectResult ToObjectResult(this WorkOrderPriorityException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                WorkOrderPriorityAlreadyExistException => StatusCodes.Status409Conflict,
                WorkOrderPriorityNotFoundException => StatusCodes.Status404NotFound,
                UnhandledWorkOrderPriorityException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("WorkOrderPriority error handler not implemented")
            }
        };
    }
}

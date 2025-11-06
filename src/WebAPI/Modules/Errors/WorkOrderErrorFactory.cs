using Application.WorkOrders.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class WorkOrderErrorFactory
{
    public static ObjectResult ToObjectResult(this WorkOrderException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                WorkOrderAlreadyExistException => StatusCodes.Status409Conflict,
                WorkOrderNotFoundException => StatusCodes.Status404NotFound,
                UnhandledWorkOrderException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("WorkOrder error handler not implemented")
            }
        };
    }
}

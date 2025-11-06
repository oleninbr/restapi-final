using Application.ConditionerStatuses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class ConditionerStatusErrorFactory
{
    public static ObjectResult ToObjectResult(this ConditionerStatusException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                ConditionerStatusAlreadyExistException => StatusCodes.Status409Conflict,
                ConditionerStatusNotFoundException => StatusCodes.Status404NotFound,
                UnhandledConditionerStatusException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("ConditionerStatus error handler not implemented")
            }
        };
    }
}

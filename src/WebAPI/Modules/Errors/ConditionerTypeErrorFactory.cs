using Application.ConditionerTypes.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class ConditionerTypeErrorFactory
{
    public static ObjectResult ToObjectResult(this ConditionerTypeException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                ConditionerTypeAlreadyExistException => StatusCodes.Status409Conflict,
                ConditionerTypeNotFoundException => StatusCodes.Status404NotFound,
                UnhandledConditionerTypeException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("ConditionerType error handler not implemented")
            }
        };
    }
}

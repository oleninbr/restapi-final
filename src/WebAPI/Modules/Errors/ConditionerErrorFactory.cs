using Application.Conditioners.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Modules.Errors;

public static class ConditionerErrorFactory
{
    public static ObjectResult ToObjectResult(this ConditionerException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                ConditionerAlreadyExistException => StatusCodes.Status409Conflict,
                ConditionerNotFoundException => StatusCodes.Status404NotFound,
                UnhandledConditionerException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Conditioner error handler not implemented")
            }
        };
    }
}

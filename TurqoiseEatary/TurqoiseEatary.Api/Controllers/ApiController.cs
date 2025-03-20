using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace TurqoiseEatary.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{

    protected IActionResult Problem(List<Error> errors)
    {

        if (errors.Count is 0)
        {
            Problem();
        }
        if (errors.All(errors => errors.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        if (errors.All(errors => errors.NumericType == 2))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items["errors"] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };
        return Problem(statusCode: statusCode, title: error.Description, detail: error.Code);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description
            );
        }
        return ValidationProblem(modelStateDictionary);
    }

}
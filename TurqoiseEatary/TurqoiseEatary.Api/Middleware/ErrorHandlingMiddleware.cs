using System.Net;
using System.Text.Json;
using ErrorOr;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TurqoiseEatary.Api.Middleware;
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }

    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        var errors = context?.Items["errors"] as List<Error>;
        var response = context.Response;
        response.ContentType = "application/Json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorResponse = new
        {
            Message = "An error occurred. Please try again later.",
            Error = exception.Message,
            Errors = errors?.Select(e => e.Code ?? "Unknown error").ToList()
        };


        var result = JsonSerializer.Serialize(errorResponse);
        return response.WriteAsync(result);

    }
}

using ArzonOL.Exceptions;

namespace ArzonOL.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (NotFoundException e)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(new { error = e.Message });
        }
        catch (BadRequestException e)
        {
            httpContext.Response.StatusCode = e.ErrorCode;
            await httpContext.Response.WriteAsJsonAsync(new { error = e.Message });
        }
        catch (Exception e)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { error = e.Message });

            Console.WriteLine(e + "Internal error");
            throw;
        }
    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
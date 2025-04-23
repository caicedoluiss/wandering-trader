using WanderingTrader.WebAPI.Models;

namespace WanderingTrader.WebAPI.Middlewares;

public class ExceptionsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var message = ex.ToString();
            var response = new ErrorResponse(StatusCodes.Status500InternalServerError, $"Unexpected Application Error:{Environment.NewLine}{message}");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
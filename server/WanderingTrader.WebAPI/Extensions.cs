using System.Net.Mime;
using WanderingTrader.Application;
using WanderingTrader.WebAPI.Endpoints;
using WanderingTrader.WebAPI.Models;

namespace WanderingTrader.WebAPI;

internal static class Extensions
{
    internal static void MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app, string? basePath = null) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app, basePath);
    }

    public static bool IsDebug(this IWebHostEnvironment webHostEnvironment)
    {
        return webHostEnvironment.IsEnvironment(AppEnvironment.Debug.ToString());
    }

    public static bool IsLocal(this IWebHostEnvironment webHostEnvironment)
    {
        return webHostEnvironment.IsEnvironment(AppEnvironment.Local.ToString());
    }

    public static RouteHandlerBuilder WithCommonConventions(this RouteHandlerBuilder route)
    {
        return route
         .Produces<ErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
         .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);
    }
}
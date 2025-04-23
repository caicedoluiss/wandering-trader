using Microsoft.AspNetCore.Mvc;
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Application.UseCases.Products.Queries;
using WanderingTrader.WebAPI.Models;

namespace WanderingTrader.WebAPI.Endpoints.Products;

public class GetProductsEndpoint : IEndpoint
{
    public record GetProductsResponse(IEnumerable<ProductDto> Products);

    public static void Map(IEndpointRouteBuilder app, string? basePath = null)
    {
        string path = Utils.BuildEndpointPath(basePath, "products");
        app.MapGet(path, Handler)
            .Produces<GetProductsResponse>()
            .WithCommonConventions()
            .WithTags(nameof(Products));
    }

    private static async Task<IResult> Handler(
        [FromServices] IAppRequestHandler<ReadProductsQuery.QueryArgs, ReadProductsQuery.QueryResult> handler,
        CancellationToken cancellationToken = default
    )
    {
        var request = new AppRequest<ReadProductsQuery.QueryArgs>(new());
        var result = await handler.HandleAsync(request, cancellationToken);

        return !result.IsSuccess ?
            Results.BadRequest(new ErrorResponse(result.ValidationErrors)) :
            Results.Ok(new GetProductsResponse(result.Value!.Products));
    }
}
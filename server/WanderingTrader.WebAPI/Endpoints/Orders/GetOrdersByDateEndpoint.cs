

using Microsoft.AspNetCore.Mvc;
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.WebAPI.Models;
using RequestArgs = WanderingTrader.Application.UseCases.Orders.Queries.ListOrdersQuery.QueryArgs;
using RequestResult = WanderingTrader.Application.UseCases.Orders.Queries.ListOrdersQuery.QueryResult;

namespace WanderingTrader.WebAPI.Endpoints.Orders;

public class GetOrdersByDateEndpoint : IEndpoint
{
    public record GetOrdersByDateResponse(IEnumerable<OrderDto> Orders);

    public static void Map(IEndpointRouteBuilder app, string? basePath = null)
    {
        string url = Utils.BuildEndpointPath(basePath, "orders/{clientDate}");
        app.MapGet(url, Handler)
            .Produces<GetOrdersByDateResponse>()
            .WithCommonConventions()
            .WithTags(nameof(Orders));
    }

    private static async Task<IResult> Handler([FromRoute] string clientDate,
        [FromServices] IAppRequestHandler<RequestArgs, RequestResult> handler,
        CancellationToken cancellationToken = default)
    {
        var request = new AppRequest<RequestArgs>(new(clientDate));
        var result = await handler.HandleAsync(request, cancellationToken);

        return !result.IsSuccess ?
            Results.BadRequest(new ErrorResponse(result.ValidationErrors)) :
            Results.Ok(new GetOrdersByDateResponse(result.Value!.Orders));
    }
}
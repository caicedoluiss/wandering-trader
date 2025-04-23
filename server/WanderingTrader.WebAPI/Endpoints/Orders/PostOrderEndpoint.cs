using Microsoft.AspNetCore.Mvc;
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Application.UseCases.Orders.Commands;
using WanderingTrader.WebAPI.Models;

namespace WanderingTrader.WebAPI.Endpoints.Orders;

public class PostOrderEndpoint : IEndpoint
{
    public record PostOrderRequest() : NewOrderDto;
    public record PostOrderResponse(string Id);

    public static void Map(IEndpointRouteBuilder app, string? basePath = null)
    {
        var path = Utils.BuildEndpointPath(basePath, "orders");
        app.MapPost(path, Handler)
            .Produces<PostOrderResponse>()
            .WithCommonConventions()
            .WithTags(nameof(Orders));
    }

    public static async Task<IResult> Handler([FromBody] PostOrderRequest body,
        IAppRequestHandler<CreateOrderCommand.CommandArgs, CreateOrderCommand.CommandResult> requestHandler,
        CancellationToken cancellationToken = default)
    {
        var request = new AppRequest<CreateOrderCommand.CommandArgs>(new(body));
        var result = await requestHandler.HandleAsync(request, cancellationToken);

        return !result.IsSuccess ?
            Results.BadRequest(new ErrorResponse(result.ValidationErrors)) :
            Results.Ok(new PostOrderResponse(result.Value!.Id));
    }
}
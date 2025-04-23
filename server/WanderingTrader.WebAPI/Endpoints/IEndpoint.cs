namespace WanderingTrader.WebAPI.Endpoints;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app, string? basePath = null);
}
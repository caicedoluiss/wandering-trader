using Microsoft.Extensions.DependencyInjection;
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.UseCases.Orders;
using WanderingTrader.Application.UseCases.Orders.Commands;
using WanderingTrader.Application.UseCases.Orders.Queries;
using WanderingTrader.Application.UseCases.Products;
using WanderingTrader.Application.UseCases.Products.Queries;

namespace WanderingTrader.Application;

public static class ConfigureApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        AddEntityMappers(services);
        AddQueries(services);
        AddCommands(services);
    }

    private static void AddEntityMappers(IServiceCollection services)
    {
        services.AddSingleton<IProductMapper, ProductsMapper>();
        services.AddSingleton<IOrderMapper, OrdersMapper>();
    }

    private static void AddQueries(IServiceCollection services)
    {
        services.AddScoped<IAppRequestHandler<ReadProductsQuery.QueryArgs, ReadProductsQuery.QueryResult>, ReadProductsQuery.Handler>();
        services.AddScoped<IAppRequestHandler<ListOrdersQuery.QueryArgs, ListOrdersQuery.QueryResult>, ListOrdersQuery.Handler>();
    }

    private static void AddCommands(IServiceCollection services)
    {
        services.AddScoped<IAppRequestHandler<CreateOrderCommand.CommandArgs, CreateOrderCommand.CommandResult>, CreateOrderCommand.Handler>();
    }
}
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WanderingTrader.Application.UseCases.Orders.Readers;
using WanderingTrader.Application.UseCases.Orders.Writers;
using WanderingTrader.Application.UseCases.Products.Readers;
using WanderingTrader.Infrastructure.DbReaders;
using WanderingTrader.Infrastructure.DbWriters;

namespace WanderingTrader.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        CosmosConfig cosmosConfig = configuration.GetSection(CosmosConfig.Position).Get<CosmosConfig>() ?? new();
        services.AddDbContext<AppDbContext>(config => config.UseCosmos(cosmosConfig.Endpoint, cosmosConfig.Token, cosmosConfig.DbName));

        AddDbReaders(services);
        AddDbWriters(services);
    }

    private static void AddDbReaders(IServiceCollection services)
    {
        services.AddScoped<IReadProductsDbReader, ReadProductsDbReader>();
        services.AddScoped<IListOrdersReader, ListOrdersDbReader>();
    }

    private static void AddDbWriters(IServiceCollection services)
    {
        services.AddScoped<ICreaterOrderDbWriter, CreateOrderDbWriter>();
    }
}
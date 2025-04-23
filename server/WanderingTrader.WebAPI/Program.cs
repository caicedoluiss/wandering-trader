using WanderingTrader.Infrastructure;
using WanderingTrader.WebAPI;
using WanderingTrader.WebAPI.Endpoints.Products;
using WanderingTrader.WebAPI.Middlewares;
using WanderingTrader.Application;
using WanderingTrader.WebAPI.Endpoints.Orders;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using WanderingTrader.WebAPI.Endpoints;

/// <summary>
/// HTTPS launchSettings configuration
/// "https": {
///    "commandName": "Project",
//     "dotnetRunMessages": true,
//     "launchBrowser": true,
//     "applicationUrl": "https://localhost:7163;http://localhost:5117",
//     "environmentVariables": {
//         "ASPNETCORE_ENVIRONMENT": "Development"
//     }
// },
/// 
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();

        builder.Services.AddSingleton<ExceptionsMiddleware>();

        if (builder.Environment.IsDebug() || builder.Environment.IsLocal() || builder.Environment.IsDevelopment())
        {
            builder.Services.AddSwaggerGen();
        }

        var app = builder.Build();

        if (args.Contains("migrations")) // No migrations actually, only setup and db seeding
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (context.Database.EnsureCreated())
            {
                Console.WriteLine("Setting up database and data seeding...");
                SeedData.InitializeAsync(context);
                Console.WriteLine("Database seeding complete...");

            }
            return;
        }

        if (builder.Environment.IsDebug() || builder.Environment.IsLocal() || builder.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                string endpointPrefix = string.Empty;
                config.SwaggerEndpoint($"{endpointPrefix}/swagger/v1/swagger.json", "Wandering Trader WebAPI");
                config.RoutePrefix = string.Empty;
            });

            app.UseCors(config =>
            {
                config.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ExceptionsMiddleware>();

        string baseUrl = "/api/v1";

        app.MapEndpoint<GetProductsEndpoint>(baseUrl);
        app.MapEndpoint<PostOrderEndpoint>(baseUrl);
        app.MapEndpoint<GetOrdersByDateEndpoint>(baseUrl);

        app.Run();
    }
}
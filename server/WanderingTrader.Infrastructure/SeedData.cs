using WanderingTrader.Core;

namespace WanderingTrader.Infrastructure;

public static class SeedData
{
    private static readonly Product[] products = [
        new()
        {
            Name = "Tortilla de Pollo",
            Cost = 2450,
            Price = 5000
        },
        new()
        {
            Name = "Tortilla de Cerdo Agridulce",
            Cost = 2800,
            Price = 5500
        },
        new()
        {
            Name = "Tortilla Hawaiana",
            Cost = 1800,
            Price = 3500
        },
        new()
        {
            Name = "Caja Mini Perro",
            Cost = 140,
            Price = 0
        },
        new()
        {
            Name = "Caja Domicilio",
            Cost = 700,
            Price = 0
        },
        new()
        {
            Name = "Cervilleta",
            Cost = 15,
            Price = 0
        },
        new()
        {
            Name = "Adicional Queso",
            Cost = 510,
            Price = 1000
        },
        new()
        {
            Name = "Adicional Jamon",
            Cost = 350,
            Price = 1000,
        },
        new()
        {
            Name = "Adicional Piña",
            Cost = 200,
            Price = 1000
        },
        new()
        {
            Name = "Adicional Pico e' Gallo",
            Cost = 250,
            Price = 1000
        },
        new()
        {
            Name = "Adicional Salsa Agria + Recipiente",
            Cost = 300,
            Price = 0
        },
        new()
        {
            Name = "Adicional Salsa Picante + Recipiente",
            Cost = 300,
            Price = 0
        },
        new()
        {
            Name = "Manzana Postobon 250ml",
            Cost = 900,
            Price = 1500
        },
        new()
        {
            Name = "Pepsi Zero 250ml",
            Cost = 900,
            Price = 1500
        },
        new()
        {
            Name = "Agua Cristal 300ml",
            Cost = 500,
            Price = 1000
        },
        new()
        {
            Name = "Pago Mano de Obra",
            Cost = 20000,
            Price = 0,
            AdminOnlyVisibility = true,

        },
    ];

    public static void InitializeAsync(AppDbContext context)
    {
        context.AddRange(products);
        context.SaveChanges();
    }
}
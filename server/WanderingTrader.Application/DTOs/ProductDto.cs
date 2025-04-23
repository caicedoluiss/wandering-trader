namespace WanderingTrader.Application.DTOs;

public record ProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public bool AdminOnlyVisibility { get; set; }
}
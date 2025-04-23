using WanderingTrader.Core;

namespace WanderingTrader.Application.DTOs;

public record OrderDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool NoCharge { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public IEnumerable<OrderItem> Items { get; set; } = [];
}

public record NewOrderDto
{
    /// <summary>
    /// ISO 8601 compliant format.
    /// </summary>
    public string ClientDate { get; set; } = string.Empty;
    public bool NoCharge { get; set; }
    public string? PaymentMethod { get; set; }
    public IEnumerable<OrderItem> Items { get; set; } = [];
}
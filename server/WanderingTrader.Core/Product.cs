namespace WanderingTrader.Core;

public class Product : DbEntityBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public bool AdminOnlyVisibility { get; set; }

    public decimal NetIncome => Price - Cost;
}
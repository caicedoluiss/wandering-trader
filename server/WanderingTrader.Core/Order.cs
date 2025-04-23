using System;
using System.Collections.Generic;
using System.Linq;

namespace WanderingTrader.Core;

public class Order : DbEntityBase, IDbEntity
{
    public DateTime Date { get; set; }
    public bool NoCharge { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public IEnumerable<OrderItem>? Items { get; set; }
}

public class OrderItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public uint Quantity { get; set; } = 1;
}
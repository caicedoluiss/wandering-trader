using System.Globalization;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Orders;

public class OrdersMapper : IOrderMapper
{
    public Order Map(NewOrderDto fromEntity, Order? toEntity = null)
    {
        var result = toEntity ?? new();

        //TODO: Validar conversion DateTimeOffset (vs dateTimeOffset.UtcDateTime.AddTicks(dateTimeOffset.Offset.Ticks);)
        DateTimeOffset clientDate = DateTimeOffset.ParseExact(fromEntity.ClientDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        result.Date = clientDate.UtcDateTime;
        System.Console.WriteLine(result.Date.ToString(Constants.DateTimeFormat));
        System.Console.WriteLine(clientDate.UtcDateTime.AddTicks(clientDate.Offset.Ticks).ToString(Constants.DateTimeFormat));
        result.NoCharge = fromEntity.NoCharge;
        result.PaymentMethod = !fromEntity.NoCharge ? Enum.Parse<PaymentMethod>(fromEntity.PaymentMethod ?? string.Empty) : null;
        result.Items = fromEntity.Items;

        return result;
    }

    public OrderDto Map(Order fromEntity, OrderDto? toEntity = null)
    {
        var result = toEntity ?? new();

        result.Id = fromEntity.Id;
        result.Date = fromEntity.Date;
        result.Items = fromEntity.Items ?? [];
        result.NoCharge = fromEntity.NoCharge;
        result.PaymentMethod = fromEntity.PaymentMethod;

        return result;
    }
}
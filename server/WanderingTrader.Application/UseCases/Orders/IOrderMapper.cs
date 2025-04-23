using WanderingTrader.Application.DTOs;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Orders;

public interface IOrderMapper :
    IEntityMapper<NewOrderDto, Order>,
    IEntityMapper<Order, OrderDto>
{

}
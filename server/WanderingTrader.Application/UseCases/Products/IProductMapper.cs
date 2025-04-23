using WanderingTrader.Application.DTOs;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Products;

public interface IProductMapper : IEntityMapper<Product, ProductDto>
{
}
using WanderingTrader.Application.DTOs;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Products;

public class ProductsMapper : IProductMapper
{
    public ProductDto Map(Product fromEntity, ProductDto? toEntity = null)
    {
        var result = toEntity ?? new();
        result.Id = fromEntity.Id;
        result.Name = fromEntity.Name;
        result.Cost = fromEntity.Cost;
        result.Price = fromEntity.Price;
        result.AdminOnlyVisibility = fromEntity.AdminOnlyVisibility;

        return result;
    }
}
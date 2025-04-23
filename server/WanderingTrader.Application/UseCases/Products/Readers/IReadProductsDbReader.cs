using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.UseCases.Products.Queries;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Products.Readers;

public interface IReadProductsDbReader : IDbReaderHandler<IReadProductsDbReader.ReaderArgs, IReadProductsDbReader.ReaderResult>
{
    public record ReaderArgs(ReadProductsQuery.QueryArgs Args);
    public record ReaderResult(IReadOnlyCollection<Product> Products);
}
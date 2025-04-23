using WanderingTrader.Application.CQRS;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Orders.Readers;

public interface IListOrdersReader : IDbReaderHandler<IListOrdersReader.ReaderArgs, IListOrdersReader.ReaderResult>
{
    public record ReaderArgs(DateTimeOffset ClientDate, bool Track = false);
    public record ReaderResult(IReadOnlyCollection<Order> Orders);
}
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Orders.Writers;

public interface ICreaterOrderDbWriter : IDbWriterHandler<ICreaterOrderDbWriter.WriterArgs, ICreaterOrderDbWriter.WriterResult>
{
    public record WriterArgs(Order NewOrder);
    public record WriterResult(Order Order);
}
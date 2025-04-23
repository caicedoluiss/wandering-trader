using System.Threading;
using System.Threading.Tasks;
using WanderingTrader.Application.UseCases.Orders.Writers;

namespace WanderingTrader.Infrastructure.DbWriters;

public class CreateOrderDbWriter : ICreaterOrderDbWriter
{
    private readonly AppDbContext context;

    public CreateOrderDbWriter(AppDbContext context)
    {
        this.context = context;
    }

    public Task<ICreaterOrderDbWriter.WriterResult> HandleAsync(ICreaterOrderDbWriter.WriterArgs args, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HandleWrite(args));
    }

    public ICreaterOrderDbWriter.WriterResult HandleWrite(ICreaterOrderDbWriter.WriterArgs args)
    {
        var orderResult = context.Orders.Add(args.NewOrder).Entity;
        _ = context.SaveChanges();

        return new(orderResult);
    }
}
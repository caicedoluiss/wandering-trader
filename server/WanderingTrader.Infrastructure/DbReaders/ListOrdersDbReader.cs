using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WanderingTrader.Application.UseCases.Orders.Readers;
using WanderingTrader.Core;

namespace WanderingTrader.Infrastructure.DbReaders;

public class ListOrdersDbReader : IListOrdersReader
{
    private readonly AppDbContext context;

    public ListOrdersDbReader(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IListOrdersReader.ReaderResult> HandleAsync(IListOrdersReader.ReaderArgs args, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = args.Track ? context.Orders : context.Orders.AsNoTracking();

        DateTime clientDateStartDateUtc = args.ClientDate.Date.ToUniversalTime();
        DateTime clientDateEndDateUtc = args.ClientDate.Date.AddDays(1).ToUniversalTime();

        query = query.Where(o => o.Date >= clientDateStartDateUtc && o.Date < clientDateEndDateUtc);

        var t = await query.ToListAsync(cancellationToken);

        return new(await query.ToListAsync(cancellationToken));
    }

    public IListOrdersReader.ReaderResult HandleRead(IListOrdersReader.ReaderArgs args)
    {
        IQueryable<Order> query = args.Track ? context.Orders : context.Orders.AsNoTracking();

        DateTime clientDateStartDateUtc = args.ClientDate.Date.ToUniversalTime();
        DateTime clientDateEndDateUtc = args.ClientDate.Date.AddDays(1).ToUniversalTime();

        query = query.Where(o => o.Date >= clientDateStartDateUtc && o.Date < clientDateEndDateUtc);

        return new(query.ToList());
    }
}
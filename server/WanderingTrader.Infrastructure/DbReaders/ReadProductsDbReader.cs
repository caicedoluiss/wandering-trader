using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WanderingTrader.Application.UseCases.Products.Readers;

namespace WanderingTrader.Infrastructure.DbReaders;

public class ReadProductsDbReader : IReadProductsDbReader
{
    private readonly AppDbContext context;

    public ReadProductsDbReader(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IReadProductsDbReader.ReaderResult> HandleAsync(IReadProductsDbReader.ReaderArgs args, CancellationToken cancellationToken = default)
    {
        var products = await context.Products.AsNoTracking().ToListAsync(cancellationToken);
        return new(products.AsReadOnly());
    }

    public IReadProductsDbReader.ReaderResult HandleRead(IReadProductsDbReader.ReaderArgs args)
    {
        return new(context.Products.AsNoTracking().ToArray().AsReadOnly());
    }
}
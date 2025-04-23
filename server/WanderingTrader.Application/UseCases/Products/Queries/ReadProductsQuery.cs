using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Application.UseCases.Products.Readers;

namespace WanderingTrader.Application.UseCases.Products.Queries;

public class ReadProductsQuery
{
    public record QueryArgs();
    public record QueryResult(IEnumerable<ProductDto> Products);

    public class Handler : AppRequestHandler<QueryArgs, QueryResult>
    {
        private readonly IReadProductsDbReader reader;
        private readonly IProductMapper mapper;

        public Handler(IReadProductsDbReader reader, IProductMapper mapper)
        {
            this.reader = reader;
            this.mapper = mapper;
        }

        protected override async Task<QueryResult> ExecuteAsync(QueryArgs args, CancellationToken cancellationToken = default)
        {
            var readerResult = await reader.HandleAsync(new(args), cancellationToken);

            var products = readerResult.Products?.Select(x => mapper.Map(x)) ?? [];

            return new(products);
        }

        protected override Task<IEnumerable<(string fieldName, string errorMessage)>> ValidateAsync(QueryArgs args, CancellationToken cancellationToken = default)
        {
            return NoValidationErrors;
        }
    }
}
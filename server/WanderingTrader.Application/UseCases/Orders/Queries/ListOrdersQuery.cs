using System.Globalization;
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Application.UseCases.Orders.Readers;

namespace WanderingTrader.Application.UseCases.Orders.Queries;

public class ListOrdersQuery
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ClientDate">ISO 8601 compliant format./param>
    public record QueryArgs(string ClientDate);
    public record QueryResult(IEnumerable<OrderDto> Orders);

    public class Handler : AppRequestHandler<QueryArgs, QueryResult>
    {
        private readonly IListOrdersReader reader;
        private readonly IOrderMapper mapper;

        public Handler(IListOrdersReader reader, IOrderMapper mapper)
        {
            this.reader = reader;
            this.mapper = mapper;
        }

        protected override async Task<QueryResult> ExecuteAsync(QueryArgs args, CancellationToken cancellationToken = default)
        {
            DateTimeOffset clientDate = DateTimeOffset.ParseExact(args.ClientDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            var readerResult = await reader.HandleAsync(new(clientDate));

            return new(readerResult.Orders.Select(o => mapper.Map(o)));
        }

        protected override Task<IEnumerable<(string fieldName, string errorMessage)>> ValidateAsync(QueryArgs args, CancellationToken cancellationToken = default)
        {
            var errors = new List<(string, string)>();
            if (!DateTimeOffset.TryParseExact(args.ClientDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                errors.Add(("clientDate", "Invalid date format"));

            return Task.FromResult(errors.AsEnumerable());
        }
    }
}
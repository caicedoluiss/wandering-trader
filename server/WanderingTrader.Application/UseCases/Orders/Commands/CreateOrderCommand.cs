using System.Globalization;
using WanderingTrader.Application.CQRS;
using WanderingTrader.Application.DTOs;
using WanderingTrader.Application.UseCases.Orders.Writers;
using WanderingTrader.Core;

namespace WanderingTrader.Application.UseCases.Orders.Commands;

public class CreateOrderCommand
{
    public record CommandArgs(NewOrderDto NewOrder);
    public record CommandResult(string Id);

    public class Handler : AppRequestHandler<CommandArgs, CommandResult>
    {
        private readonly IOrderMapper mapper;
        private readonly ICreaterOrderDbWriter writer;

        public Handler(IOrderMapper mapper, ICreaterOrderDbWriter writer)
        {
            this.mapper = mapper;
            this.writer = writer;
        }

        protected override Task<CommandResult> ExecuteAsync(CommandArgs args, CancellationToken cancellationToken = default)
        {
            var order = mapper.Map(args.NewOrder);
            var writeResult = writer.HandleWrite(new(order));

            return Task.FromResult(new CommandResult(writeResult.Order.Id));
        }

        protected override Task<IEnumerable<(string fieldName, string errorMessage)>> ValidateAsync(CommandArgs args, CancellationToken cancellationToken = default)
        {
            var order = args.NewOrder;
            var errors = new List<(string, string)>();
            if (!DateTimeOffset.TryParseExact(order.ClientDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                errors.Add(("date", "Date format is not valid."));
            }

            if (!order.Items.Any()) errors.Add(("items", "Order items must contain at least one element."));
            if (order.Items.Any(a => string.IsNullOrEmpty(a.Id))) errors.Add(("items", "Order items id is required."));
            if (order.Items?.Select(x => x.Id).Distinct().Count() != order.Items?.Count()) errors.Add(("items", "Order items id must be unique."));
            if (!order.NoCharge && order.PaymentMethod is null) errors.Add(("paymentMethod", "Payment method required."));
            if (!string.IsNullOrEmpty(order.PaymentMethod) && Enum.TryParse<PaymentMethod>(order.PaymentMethod, out var paymentMethod) && !Enum.IsDefined(paymentMethod)) errors.Add(("paymentMethod", "Payment method is not valid."));
            return Task.FromResult(errors.AsEnumerable());
        }
    }
}
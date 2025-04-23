namespace WanderingTrader.Application.CQRS;

public record AppRequest<TArgs>(TArgs Args) : IAppRequest<TArgs>;

namespace WanderingTrader.Application.CQRS
{
    public interface IAppRequest<out TArgs>
    {
        TArgs Args { get; }
    }
}
namespace WanderingTrader.Application.CQRS
{
    public interface ITaskHandler<in TArgs, TResult>
    {
        Task<TResult> HandleAsync(TArgs args, CancellationToken cancellationToken = default);
    }
}
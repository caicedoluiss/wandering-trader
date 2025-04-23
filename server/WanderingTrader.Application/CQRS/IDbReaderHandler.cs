namespace WanderingTrader.Application.CQRS
{
    public interface IDbReaderHandler<in TArgs, TResult> : ITaskHandler<TArgs, TResult>
    {
        TResult HandleRead(TArgs args);
    }
}
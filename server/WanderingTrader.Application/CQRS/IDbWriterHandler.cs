namespace WanderingTrader.Application.CQRS
{
    public interface IDbWriterHandler<TArgs, TResult> : ITaskHandler<TArgs, TResult>
    {
        TResult HandleWrite(TArgs args);
    }
}
namespace WanderingTrader.Application.CQRS
{
    public interface IAppRequestHandler<in TArgs, TResult> : ITaskHandler<IAppRequest<TArgs>, IAppRequestResult<TResult>> where TResult : class { }

    public interface IAppRequestHandler<in TArgs> : ITaskHandler<IAppRequest<TArgs>, IAppRequestResultBase> { }
}
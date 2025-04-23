namespace WanderingTrader.Application.CQRS
{
    public interface IAppRequestResult<out TValue> : IAppRequestResultBase where TValue : class
    {
        TValue? Value { get; }
    }
}
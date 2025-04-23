namespace WanderingTrader.Application.CQRS
{
    public interface IAppRequestResultBase
    {
        bool IsSuccess { get; }
        IEnumerable<(string fieldName, string errorMessage)> ValidationErrors { get; }
        string Message { get; }
    }
}
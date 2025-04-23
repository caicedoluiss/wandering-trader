namespace WanderingTrader.Application.CQRS
{
    public class AppRequestResultBase : IAppRequestResultBase
    {
        public bool IsSuccess { get; } = false;
        public IEnumerable<(string fieldName, string errorMessage)> ValidationErrors { get; } = [];
        public string Message { get; } = string.Empty;

        public AppRequestResultBase(bool isSuccess = false)
        {
            IsSuccess = isSuccess;
        }

        public AppRequestResultBase(bool isSuccess, IEnumerable<(string, string)> validationErrors, string message)
        {
            IsSuccess = isSuccess;
            ValidationErrors = validationErrors;
            Message = message;
        }

        public AppRequestResultBase(IEnumerable<(string, string)> validationErrors) : this(false, validationErrors, "There are some validation errors.") { }
    }
}
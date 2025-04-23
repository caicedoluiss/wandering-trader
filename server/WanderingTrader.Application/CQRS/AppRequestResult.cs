namespace WanderingTrader.Application.CQRS
{
    public class AppRequestResult<TValue> : AppRequestResultBase, IAppRequestResult<TValue> where TValue : class
    {
        public TValue? Value { get; } = null;

        public AppRequestResult() { }

        public AppRequestResult(TValue? value, IEnumerable<(string, string)> validationErrors, string message) : base(value != null, validationErrors, message)
        {
            Value = value;
        }

        public AppRequestResult(IEnumerable<(string, string)> validationErrors) : this(null, validationErrors, "There are some validation errors.") { }
        public AppRequestResult(TValue value) : this(value, [], string.Empty) { }
    }
}
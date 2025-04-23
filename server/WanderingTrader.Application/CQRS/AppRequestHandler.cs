namespace WanderingTrader.Application.CQRS
{
    public abstract class AppRequestHandler<TArgs, TResult> : IAppRequestHandler<TArgs, TResult> where TResult : class
    {
        protected static readonly Task<IEnumerable<(string, string)>> NoValidationErrors = Task.FromResult<IEnumerable<(string, string)>>([]);

        protected abstract Task<IEnumerable<(string fieldName, string errorMessage)>> ValidateAsync(TArgs args, CancellationToken cancellationToken = default);
        protected abstract Task<TResult> ExecuteAsync(TArgs args, CancellationToken cancellationToken = default);

        public async Task<IAppRequestResult<TResult>> HandleAsync(IAppRequest<TArgs> appRequest, CancellationToken cancellationToken = default)
        {
            var validationErrors = await ValidateAsync(appRequest.Args, cancellationToken);
            if (validationErrors.Any()) return new AppRequestResult<TResult>(validationErrors);

            var executionResult = await ExecuteAsync(appRequest.Args, cancellationToken);

            return new AppRequestResult<TResult>(executionResult);
        }
    }

    public abstract class AppRequestHandler<TArgs> : IAppRequestHandler<TArgs>
    {
        protected abstract Task<IEnumerable<(string fieldName, string errorMessage)>> ValidateAsync(TArgs args, CancellationToken cancellationToken = default);
        protected abstract Task<IAppRequestResultBase> ExecuteAsync(TArgs args, CancellationToken cancellationToken = default);

        public async Task<IAppRequestResultBase> HandleAsync(IAppRequest<TArgs> appRequest, CancellationToken cancellationToken = default)
        {
            var validationErrors = await ValidateAsync(appRequest.Args, cancellationToken);
            if (validationErrors.Any()) return new AppRequestResultBase(validationErrors);

            _ = await ExecuteAsync(appRequest.Args, cancellationToken);

            return new AppRequestResultBase(true);
        }
    }
}
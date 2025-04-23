namespace WanderingTrader.WebAPI.Models;

public record ErrorResponse
{
    public int StatusCode { get; }
    public string Message { get; } = string.Empty;
    public IEnumerable<FieldValidationError> ValidationErrors { get; } = [];

    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public ErrorResponse(string message, IEnumerable<(string propertyName, string errorMessage)> validationErrors) : this(400, message)
    {
        ValidationErrors = validationErrors
            .GroupBy(x => x.propertyName, x => x.errorMessage, (name, errors) => new FieldValidationError(name, errors));
    }

    public ErrorResponse(IEnumerable<(string, string)> validationErrors) : this("There are some properties validation errors.", validationErrors)
    {
    }

}
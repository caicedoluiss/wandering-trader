namespace WanderingTrader.WebAPI.Models;

public record FieldValidationError(string FieldName, IEnumerable<string> Errors);
namespace ApiService.Validators;

public record ValidationError(string PropertyName, string ErrorMessage);
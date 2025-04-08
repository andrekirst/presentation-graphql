using ApiService.Validators;

namespace ApiService.Exceptions;

public class ValidationException(IEnumerable<ValidationError>? errors) : Exception
{
    public IEnumerable<ValidationError>? Errors { get; set; } = errors;
}
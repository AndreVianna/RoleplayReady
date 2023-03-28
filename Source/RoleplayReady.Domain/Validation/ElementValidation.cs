namespace RoleplayReady.Domain.Validation;

public record ElementValidation
{
    public ValidationSeverityLevel Severity { get; init; }
    public Func<Element, bool> Validate { get; init; } = _ => true;
    public string Message { get; init; } = string.Empty;
}
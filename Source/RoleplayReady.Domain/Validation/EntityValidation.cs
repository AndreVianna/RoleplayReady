namespace RoleplayReady.Domain.Validation;

public record EntityValidation
{
    public ValidationSeverityLevel Severity { get; init; }
    public Func<Entity, bool> Validate { get; init; } = _ => true;
    public string Message { get; init; } = string.Empty;
}
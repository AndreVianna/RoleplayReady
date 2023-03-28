namespace RoleplayReady.Domain.Models;

public record Validation
{
    public ValidationSeverityLevel Severity { get; init; }
    public Func<GameEntity, bool> Validate { get; init; } = _ => true;
    public string Message { get; init; } = string.Empty;
}
namespace RoleplayReady.Domain.Models.Validations;

public record Validation {
    public required Element Parent { get; init; }
    public Severity Severity { get; init; } = Suggestion;
    public required Func<IHasAttributes, bool> Validate { get; init; }
    public required string Message { get; init; }
}
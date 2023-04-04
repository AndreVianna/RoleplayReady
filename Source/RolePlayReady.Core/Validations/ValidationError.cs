namespace RolePlayReady.Validations;

public record ValidationError {
    public ValidationError() { }

    [SetsRequiredMembers]
    public ValidationError(string message, string? field = null) {
        Message = Throw.IfNullOrWhiteSpaces(message);
        Field = field;
    }

    private readonly string _message = string.Empty;
    public required string Message {
        get => _message;
        init => _message = Throw.IfNullOrWhiteSpaces(value);
    }

    public string? Field { get; init; }
}
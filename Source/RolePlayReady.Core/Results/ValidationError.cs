namespace RolePlayReady.Results;

public record ValidationError {
    private readonly string _message = string.Empty;
    public required string Message {
        get => _message;
        init => _message = Throw.IfNullOrWhiteSpaces(value);
    }

    public string? Field { get; init; }
}
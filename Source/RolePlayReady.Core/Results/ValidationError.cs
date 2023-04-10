namespace System.Results;

public record ValidationError {
    private readonly string _message = string.Empty;
    public required string Message {
        get => _message;
        init => _message = Throw.IfNullOrWhiteSpaces(value, "The value cannot be null or whitespaces.", nameof(Message));
    }

    public string? Field { get; init; }
}
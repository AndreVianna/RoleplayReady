namespace System.Results;

public record ValidationError {
    [SetsRequiredMembers]
    public ValidationError(string message, string source) {
        Message = message;
        Source = source;
    }

    public required string Message { get; init; }
    public required string Source { get; init; }
}
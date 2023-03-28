namespace RoleplayReady.Domain.Validation;

public record ValidationError
{
    public ValidationError(string message, params object?[] arguments)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Message cannot be null or empty.", nameof(message));
        }

        Message = message;
        Arguments = arguments;
    }

    public string Source { get; init; } = string.Empty;
    public string Message { get; }
    public object?[] Arguments { get; }
}
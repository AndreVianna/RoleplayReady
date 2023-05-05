namespace System.Extensions;

public static class ValidationErrorExtensions {
    public static bool Contains(this IEnumerable<ValidationError> errors, string message)
        => errors.Any(error => error.MessageTemplate == message);
}
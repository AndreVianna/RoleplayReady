namespace System.Validations;

public static class Validation {
    public static IEnumerable<ValidationError> EnsureNotNull(object? subject, string source)
        => subject is null
            ? new[] { new ValidationError(CannotBeNull, source!) }
            : Array.Empty<ValidationError>();
}

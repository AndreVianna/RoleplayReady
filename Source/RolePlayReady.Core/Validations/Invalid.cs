namespace RolePlayReady.Validations;

public record Invalid {
    public Invalid(IEnumerable<ValidationError?>? errors) {
        var validationErrors = errors as ValidationError?[] ?? errors?.ToArray() ?? Array.Empty<ValidationError?>();
        if (validationErrors.Length == 0)
            throw new ArgumentException("The error collection cannot be null or empty.", nameof(errors));

        if (validationErrors.Any(e => e is null))
            throw new ArgumentException("The error collection cannot contain null elements.", nameof(errors));

        Errors = new ReadOnlyCollection<ValidationError>(validationErrors!);
    }

    public Invalid(ValidationError? error)
        : this(new[] { error ?? throw new ArgumentNullException(nameof(error), "The error cannot be null.") }) {
    }

    public IEnumerable<ValidationError> Errors { get; }
}
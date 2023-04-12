namespace System.Results;

public record Invalid {
    public Invalid(ICollection<ValidationError> errors) {
        var validationErrors = Ensure.NotNullOrEmptyOrHasNull(errors).ToArray();
        Errors = new ReadOnlyCollection<ValidationError>(validationErrors);
    }

    public Invalid(ValidationError? error)
        : this(new[] { Ensure.NotNull(error) }) {
    }

    public ICollection<ValidationError> Errors { get; }
}
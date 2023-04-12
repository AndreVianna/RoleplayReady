namespace System.Results;

public record Failure {
    public Failure(ICollection<ValidationError> errors) {
        Errors = Ensure.NotNullOrEmptyOrHasNull(errors).ToArray();
    }

    public Failure(ValidationError? error)
        : this(new[] { Ensure.NotNull(error) }) {
    }

    public ICollection<ValidationError> Errors { get; }
}
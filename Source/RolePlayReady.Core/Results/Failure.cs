namespace System.Results;

public record Failure {
    public Failure(ICollection<ValidationError> errors) {
        Errors = Ensure.NotNullOrEmptyOrHasNull(errors).ToList();
    }

    public Failure(ValidationError? error)
        : this(new[] { Ensure.NotNull(error) }) {
    }

    public ICollection<ValidationError> Errors { get; }
}
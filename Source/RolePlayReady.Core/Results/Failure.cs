namespace System.Results;

public record Failure {
    public Failure(IEnumerable<ValidationError> validationErrors) {
        Errors = new List<ValidationError>(Ensure.NotNullOrEmptyOrHasNull(validationErrors));
    }

    public Failure(ValidationError validationError) {
        Errors = new List<ValidationError> { Ensure.NotNull(validationError) };
    }

    public ICollection<ValidationError> Errors { get; }
}
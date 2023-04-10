namespace System.Results;

public record Invalid {
    public Invalid(IEnumerable<ValidationError?>? errors) {
        var validationErrors = Throw.IfNullOrEmptyOrContainNulls(errors).ToArray();
        Errors = new ReadOnlyCollection<ValidationError>(validationErrors);
    }

    public Invalid(ValidationError? error)
        : this(new[] { Throw.IfNull(error) }) {
    }

    public IEnumerable<ValidationError> Errors { get; }
}
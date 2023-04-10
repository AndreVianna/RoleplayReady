namespace System.Results;

public static class ResultFactory {
    public static Success Success => Success.Instance;
    public static Nothing Nothing => new();
    public static Valid Valid => Valid.Instance;
    public static ValidationError Error(string message, string? field = null)
        => new() {
            Message = message,
            Field = field,
        };
    public static Invalid Invalid(IEnumerable<ValidationError?>? errors) => new(errors);
    public static Invalid Invalid(ValidationError? error) => new(error);
}
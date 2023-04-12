namespace System.Results;

public static class ResultFactory {
    public static Success Success => Success.Instance;
    public static Nothing Nothing => new();
    public static Valid Valid => Valid.Instance;
    public static ValidationError Error(string message, string source) => new(message, source);
    public static Invalid Invalid(ICollection<ValidationError> errors) => new(errors);
    public static Invalid Invalid(ValidationError? error) => new(error);
}
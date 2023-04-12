namespace System.Results;

public static class ResultFactory {
    public static Nothing Nothing => new();
    public static Success Success => Success.Instance;
    public static ValidationError Error(string message, string source) => new(message, source);
    public static Failure Invalid(ICollection<ValidationError> errors) => new(errors);
    public static Failure Invalid(ValidationError? error) => new(error);
}
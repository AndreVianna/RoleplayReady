namespace System.Results.Abstractions;

public interface IValidation {
    bool IsValid { get; }
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}
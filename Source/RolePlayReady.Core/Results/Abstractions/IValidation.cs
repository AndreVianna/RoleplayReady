namespace System.Results.Abstractions;

public interface IValidation {
    bool IsSuccess { get; }
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}
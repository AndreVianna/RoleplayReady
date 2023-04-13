namespace System.Results.Abstractions;

public interface IValidationResult : IVoidResult {
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}
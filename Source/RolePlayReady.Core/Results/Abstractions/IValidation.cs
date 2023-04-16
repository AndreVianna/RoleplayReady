namespace System.Results.Abstractions;

public interface IValidation {
    bool IsSuccessful { get; }
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}
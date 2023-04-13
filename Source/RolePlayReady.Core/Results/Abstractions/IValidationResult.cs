namespace System.Results.Abstractions;

public interface IValidationResult : IResult {
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}
namespace System.Validations.Abstractions;

public interface IValidation<out TSubject> : IFinishesValidation {
    TSubject? Subject { get; }
    string Source { get; }
    ICollection<ValidationError> Errors { get; }
}
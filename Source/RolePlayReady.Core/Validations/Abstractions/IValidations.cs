namespace System.Validations.Abstractions;

public interface IValidations {
    string Source { get; }
    List<ValidationError> Errors { get; }
}

public interface IValidations<out TSubject> : IValidations {
    TSubject? Subject { get; }
}
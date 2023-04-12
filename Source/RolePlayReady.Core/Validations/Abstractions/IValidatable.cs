namespace System.Validations.Abstractions;

public interface IValidatable {
    IResult<TSubject> Validate<TSubject>();
}
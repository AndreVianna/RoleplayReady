namespace System.Validations.Abstractions;

public interface IValidatableValidations : IValidations<IValidatable> {
    ICollection<ValidationError> IsValid();
}
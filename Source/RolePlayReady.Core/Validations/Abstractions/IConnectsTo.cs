namespace System.Validations.Abstractions;

public interface IConnectsTo<out TValidation> {
    TValidation And { get; }
}
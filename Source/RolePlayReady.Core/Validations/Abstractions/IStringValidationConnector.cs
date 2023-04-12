namespace System.Validations.Abstractions;

public interface IStringValidationConnector<out TBuilder> {
    TBuilder And { get; }
    ValidationResult Result { get; }
}
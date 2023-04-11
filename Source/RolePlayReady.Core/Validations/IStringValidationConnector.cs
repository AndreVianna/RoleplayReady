namespace System.Validations;

public interface IStringValidationConnector<out TBuilder> {
    TBuilder And { get; }
    ValidationError[] Errors { get; }
}
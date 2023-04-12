namespace System.Validations.Abstractions;

public interface IConnectors<out TChecks> {
    TChecks And { get; }
    ValidationResult Result { get; }
}
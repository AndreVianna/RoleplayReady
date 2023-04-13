namespace System.Validations.Abstractions;

public interface IConnectors<out TChecks> {
    TChecks And { get; }
    Validation Result { get; }
}
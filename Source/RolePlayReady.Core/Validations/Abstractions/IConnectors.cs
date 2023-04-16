namespace System.Validations.Abstractions;

public interface IConnectors<out TChecks> : IFinishValidation
    where TChecks : class, IValidations {
    TChecks And { get; }
}

namespace System.Validations.Abstractions;

public interface IValidation { }

public interface IValidation<TValidation, out TChecks>
    : IValidation,
      IValidations,
      IConnectors<TChecks>
    where TValidation : class, IValidation<TValidation, TChecks>
    where TChecks : class, IValidations {
}

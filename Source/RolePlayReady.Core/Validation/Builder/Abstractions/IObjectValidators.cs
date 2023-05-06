namespace System.Validation.Builder.Abstractions;

public interface IObjectValidators : IValidators {
    IConnectors<object?, IValidators> IsOfType<TType>();
}
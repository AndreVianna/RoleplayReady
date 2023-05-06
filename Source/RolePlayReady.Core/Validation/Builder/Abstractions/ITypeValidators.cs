namespace System.Validation.Builder.Abstractions;

public interface ITypeValidators : IValidators {
    IConnectors<Type?, TypeValidators> IsEqualTo<TType>();
}
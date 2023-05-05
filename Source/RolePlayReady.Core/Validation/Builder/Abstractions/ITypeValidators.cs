namespace System.Validation.Builder.Abstractions;

public interface ITypeValidators : IValidators<Type?, TypeValidators> {
    IConnectors<Type?, TypeValidators> IsEqualTo<TType>();
}
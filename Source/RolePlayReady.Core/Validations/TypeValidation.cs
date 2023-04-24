namespace System.Validations;

public class TypeValidation
    : Validation<Type, ITypeValidators>,
      ITypeValidation {
    public TypeValidation(Type? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ITypeValidators> IsEqualTo<TType>() {
        if (Subject is null)
            return this;
        if (Subject != typeof(TType))
            Errors.Add(new(IsNotEqual, Source, typeof(TType), Subject));
        return this;
    }
}
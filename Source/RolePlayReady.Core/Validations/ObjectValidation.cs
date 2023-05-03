namespace System.Validations;

public class ObjectValidation
    : Validation<object, IObjectValidators>,
        IObjectValidation {
    public ObjectValidation(object? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ICollectionValidators<TType>> IsOfType<TType>() {
        if (Subject is not TType)
            Errors.Add(new(IsNotOfType, Source, typeof(TType), Source?.GetType().Name ?? "null"));
        var value = Subject is TType typedValue ? typedValue : default;
        return new CollectionValidation<TType>(new[] { value }, Source)!;
    }
}
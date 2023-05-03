namespace System.Validations;

public class ObjectValidation
    : Validation<object, IObjectValidators>,
        IObjectValidation {
    public ObjectValidation(object? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<TSubject> IsOfType<TSubject>() {
        if (Subject is not TSubject)
            Errors.Add(new(IsNotOfType, Source, typeof(TSubject), Source.GetType().Name));
        var value = Subject is TSubject typedValue ? typedValue : default;
        return new Validation<TSubject>(value, Errors)!;
    }
}
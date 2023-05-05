using System.Validation.Abstractions;

namespace System.Validation;

public class ObjectValidators
    : Validators<object?, ObjectValidators>
        , IObjectValidators {

    public static ObjectValidators CreateAsOptional(object? subject, string source)
        => new(subject, source);
    public static ObjectValidators CreateAsRequired(object? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private ObjectValidators(object? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<object?, ObjectValidators>(Subject, this);
    }

    //public IConnects<TSubject> IsOfType<TSubject>() {
    //    if (Subject is not TSubject)
    //        Errors.Add(new(IsNotOfType, Source, typeof(TSubject), Source.GetType().Name));
    //    var value = Subject is TSubject typedValue ? typedValue : default;
    //    return new Connects<ValidationCommand<TSubject>>(value, Errors)!;
    //}
}
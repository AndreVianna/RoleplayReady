namespace System.Validations;

public class ObjectValidations
    : Validations<object?, ObjectValidations>
        , IObjectValidations {

    public static ObjectValidations CreateAsOptional(object? subject, string source)
        => new(subject, source);
    public static ObjectValidations CreateAsRequired(string subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private ObjectValidations(object? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<object?, ObjectValidations>(Subject, this);
    }

    //public IConnects<TSubject> IsOfType<TSubject>() {
    //    if (Subject is not TSubject)
    //        Errors.Add(new(IsNotOfType, Source, typeof(TSubject), Source.GetType().Name));
    //    var value = Subject is TSubject typedValue ? typedValue : default;
    //    return new Connects<Validation<TSubject>>(value, Errors)!;
    //}
}
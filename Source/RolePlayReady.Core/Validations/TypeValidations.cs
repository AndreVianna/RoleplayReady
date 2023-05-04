namespace System.Validations;

public class TypeValidations
    : Validations<Type?, TypeValidations>
        , ITypeValidations {
    public static TypeValidations CreateAsRequired(Type? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private TypeValidations(Type? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<Type?, TypeValidations>(Subject, this);
    }

    public IValidationsConnector<Type?, TypeValidations> IsEqualTo<TType>() {
        if (Subject != typeof(TType))
            Errors.Add(new(IsNotEqual, Source, typeof(TType), Subject));

        return Connector;
    }
}
namespace System.Validations;

public class TypeValidations : ITypeValidations {
    private readonly Connects<TypeValidations> _connector;

    public TypeValidations(Type? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<TypeValidations>(this);
    }

    public Type? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public IConnects<ITypeValidations> IsEqualTo<TType>() {
        if (Subject != typeof(TType))
            Errors.Add(new(IsNotEqual, Source, typeof(TType), Subject));

        return _connector;
    }
}
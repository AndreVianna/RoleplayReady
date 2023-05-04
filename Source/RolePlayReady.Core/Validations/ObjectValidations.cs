namespace System.Validations;

public class ObjectValidations : IObjectValidations {
    private readonly Connects<ObjectValidations> _connector;

    public ObjectValidations(object? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<ObjectValidations>(this);
    }

    public object? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    //public IConnects<TSubject> IsOfType<TSubject>() {
    //    if (Subject is not TSubject)
    //        Errors.Add(new(IsNotOfType, Source, typeof(TSubject), Source.GetType().Name));
    //    var value = Subject is TSubject typedValue ? typedValue : default;
    //    return new Connects<Validation<TSubject>>(value, Errors)!;
    //}
}
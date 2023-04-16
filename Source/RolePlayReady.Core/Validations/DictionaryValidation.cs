namespace System.Validations;

public class DictionaryValidation<TKey, TValue> :
    Validation<IDictionary<TKey, TValue>, DictionaryValidation<TKey, TValue>, IDictionaryValidations>,
    IDictionaryValidations {

    public DictionaryValidation(IDictionary<TKey, TValue> subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IDictionaryValidations> NotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any()) Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }
}
namespace System.Validations;

public class DictionaryValueValidation<TKey, TValue> :
    Validation<IDictionary<TKey, TValue>, DictionaryValueValidation<TKey, TValue>, IDictionaryValueValidations<TKey, TValue>>,
    IDictionaryValueValidations<TKey, TValue> {

    public DictionaryValueValidation(IDictionary<TKey, TValue> subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IDictionaryValueValidations<TKey, TValue>> Each(Func<TValue, IFinishValidation> validateUsing) {
        if (Subject is null) return this;
        foreach (var item in Subject) {
            var source = $"{Source}[{item.Key}]";
            foreach (var error in validateUsing(item.Value).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
                Errors.Add(error);
            }
        }

        return this;
    }
}
namespace System.Validators;

public class DictionaryValidator<TKey, TValue> :
    Validator<IDictionary<TKey, TValue>, IDictionaryChecks<TKey, TValue>, IDictionaryConnectors<TKey, TValue>>,
    IDictionaryChecks<TKey, TValue>,
    IDictionaryConnectors<TKey, TValue> {

    public DictionaryValidator(IDictionary<TKey, TValue> subject, string? source)
        : base(subject, source) {
    }

    public IDictionaryConnectors<TKey, TValue> EachItem(Func<TValue, IValidatorResult> validateUsing) {
        if (Subject is null)
            return this;
        foreach (var item in Subject) {
            var source = $"{Source}[{item.Key}]";
            foreach (var error in validateUsing(item.Value).Result.Errors) {
                error.Arguments[0] = source;
                Errors.Add(error);
            }
        }

        return this;
    }
}
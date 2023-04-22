namespace System.Validations;

public class DictionaryValidation<TKey, TValue> :
    Validation<IDictionary<TKey, TValue>, IDictionaryValidators<TKey, TValue>>,
    IDictionaryValidation<TKey, TValue> {
    public DictionaryValidation(IDictionary<TKey, TValue>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue>> IsNotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any()) Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }

    public IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue>> MinimumCountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count < size) Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue>> CountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count != size) Errors.Add(new(MustHave, Source, size, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue>> ContainsKey(TKey key) {
        if (Subject is null) return this;
        if (!Subject.ContainsKey(key)) Errors.Add(new(MustContainKey, Source, key));
        return this;
    }

    public IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue>> NotContainsKey(TKey key) {
        if (Subject is null)
            return this;
        if (Subject.ContainsKey(key)) Errors.Add(new(MustNotContainKey, Source, key));
        return this;
    }

    public IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue>> MaximumCountIs(int size) {
        if (Subject is null)
            return this;
        if (Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));
        return this;
    }

    public IFinishesValidation ForEach(Func<TValue, IFinishesValidation> validateUsing)
        => DictionaryItemValidation.ForEachItemIn(this, validateUsing, false);
}
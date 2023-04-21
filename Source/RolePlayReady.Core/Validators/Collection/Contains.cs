namespace System.Validators.Collection;

public sealed class Contains<TItem> : CollectionValidator<TItem> {
    private readonly TItem _item;

    public Contains(string source, TItem item)
        : base(source) {
        _item = item;
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation)
        => validation.Contains(_item).Result;
}

public sealed class ContainsKey<TKey, TValue> : CollectionValidator<KeyValuePair<TKey, TValue>> {
    private readonly TKey _key;

    public ContainsKey(string source, TKey key)
        : base(source) {
        _key = key;
    }

    protected override ValidationResult ValidateValue(CollectionValidation<KeyValuePair<TKey, TValue>> validation)
        => validation.Contains(_key).Result;
}
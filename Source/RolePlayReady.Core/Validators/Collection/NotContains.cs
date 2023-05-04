﻿namespace System.Validators.Collection;

public sealed class NotContains<TItem> : CollectionValidator<TItem> {
    private readonly TItem? _item;

    public NotContains(string source, TItem? item)
        : base(source) {
        _item = item;
    }

    protected override ICollection<ValidationError> ValidateValue(CollectionValidations<TItem> validation)
        => validation.NotContains(_item).Errors;
}
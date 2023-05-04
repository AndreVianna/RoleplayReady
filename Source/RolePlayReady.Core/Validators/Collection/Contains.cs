﻿namespace System.Validators.Collection;

public sealed class Contains<TItem> : CollectionValidator<TItem> {
    private readonly TItem? _item;

    public Contains(string source, TItem? item)
        : base(source) {
        _item = item;
    }

    protected override ICollection<ValidationError> ValidateValue(CollectionValidations<TItem> validation)
        => validation.Contains(_item).Errors;
}

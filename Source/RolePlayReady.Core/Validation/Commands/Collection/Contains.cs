namespace System.Validation.Commands.Collection;

public sealed class Contains<TItem> : ValidationCommand<ICollection<TItem?>> {
    private readonly TItem? _item;

    public Contains(ICollection<TItem?> subject, TItem? item, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _item = item;
    }

    public override ValidationResult Validate()
        => !Subject.Contains(_item)
            ? AddError(MustContain, _item)
            : Validation;
}

namespace System.Validation.Commands.Collection;

public sealed class NotContains<TItem> : ValidationCommand<ICollection<TItem?>> {
    private readonly TItem? _item;

    public NotContains(ICollection<TItem?> subject, TItem? item, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _item = item;
    }

    public override ValidationResult Validate()
        => Subject.Contains(_item)
            ? AddError(MustNotContain, _item)
            : Validation;
}
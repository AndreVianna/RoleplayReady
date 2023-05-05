namespace System.Validation.Commands.Collection;

public sealed class CountIs<TItem> : ValidationCommand<ICollection<TItem?>> {
    private readonly int _count;

    public CountIs(ICollection<TItem?> subject, int count, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _count = count;
    }

    public override ValidationResult Validate()
        => Subject.Count != _count
            ? AddError(MustHave, _count, Subject.Count)
            : Validation;
}
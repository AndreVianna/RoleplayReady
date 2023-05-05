namespace System.Validation.Commands.Collection;

public sealed class MaximumCountIs<TItem> : ValidationCommand<ICollection<TItem?>> {
    private readonly int _count;

    public MaximumCountIs(ICollection<TItem?> subject, int count, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _count = count;
    }

    public override ValidationResult Validate()
        => Subject.Count > _count
            ? AddError(CannotHaveMoreThan, _count)
            : Validation;
}
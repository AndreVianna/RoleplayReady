namespace System.Validation.Commands.Collection;

public sealed class IsNotEmpty<TItem> : ValidationCommand<ICollection<TItem?>> {
    public IsNotEmpty(ICollection<TItem?> subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
    }

    public override ValidationResult Validate()
        => Subject.Count == 0
            ? AddError(CannotBeEmpty)
            : Validation;
}
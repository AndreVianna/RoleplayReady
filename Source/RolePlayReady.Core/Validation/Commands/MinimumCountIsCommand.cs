namespace System.Validation.Commands;

public sealed class MinimumCountIsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {

    public MinimumCountIsCommand(ICollection<TItem?> subject, int count, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Count >= count;
        ValidationErrorMessage = MustHaveAMinimumCountOf;
        Arguments = SetArguments(count, subject.Count);
    }
}
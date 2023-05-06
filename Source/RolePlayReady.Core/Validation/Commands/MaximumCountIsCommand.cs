namespace System.Validation.Commands;

public sealed class MaximumCountIsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {

    public MaximumCountIsCommand(ICollection<TItem?> subject, int count, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Count <= count;
        ValidationErrorMessage = MustHaveAMaximumCountOf;
        Arguments = SetArguments(count, subject.Count);
    }
}
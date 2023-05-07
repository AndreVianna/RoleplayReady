namespace System.Validation.Commands;

public sealed class MaximumCountIsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {
    public MaximumCountIsCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Count <= count;
        ValidationErrorMessage = MustHaveAMaximumCountOf;
        Arguments = SetArguments(count);
    }
}
namespace System.Validation.Commands;

public sealed class MinimumCountIsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {
    public MinimumCountIsCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Count >= count;
        ValidationErrorMessage = MustHaveAMinimumCountOf;
        Arguments = SetArguments(count);
    }
}
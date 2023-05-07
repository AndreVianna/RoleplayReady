namespace System.Validation.Commands;

public sealed class CountIsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {
    public CountIsCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Count == count;
        ValidationErrorMessage = MustHaveACountOf;
        Arguments = SetArguments(count);
    }
}
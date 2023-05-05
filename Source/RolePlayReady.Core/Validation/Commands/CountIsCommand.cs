namespace System.Validation.Commands;

public sealed class CountIsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {

    public CountIsCommand(ICollection<TItem?> subject, int count, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Count == count;
        ValidationErrorMessage = MustHaveACountOf;
        ValidationArguments = AddArguments(count);
    }
}
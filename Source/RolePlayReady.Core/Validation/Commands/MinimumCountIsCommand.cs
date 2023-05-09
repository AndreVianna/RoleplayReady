namespace System.Validation.Commands;

public sealed class HasAtLeastCommand<TItem>
    : ValidationCommand {
    public HasAtLeastCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = c => ((ICollection<TItem?>)c!).Count >= count;
        ValidationErrorMessage = MustHaveAMinimumCountOf;
        GetErrorMessageArguments = c => new object?[] { count, ((ICollection<TItem?>)c!).Count };
    }
}
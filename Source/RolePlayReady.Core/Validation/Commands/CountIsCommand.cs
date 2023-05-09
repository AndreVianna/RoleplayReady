namespace System.Validation.Commands;

public sealed class HasCommand<TItem>
    : ValidationCommand {
    public HasCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = c => ((ICollection<TItem?>)c!).Count == count;
        ValidationErrorMessage = MustHaveACountOf;
        GetArguments = c => new object?[] { count, ((ICollection<TItem?>)c!).Count };
    }
}
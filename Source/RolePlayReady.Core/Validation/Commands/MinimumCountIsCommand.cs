namespace System.Validation.Commands;

public sealed class MinimumCountIsCommand<TItem>
    : ValidationCommand {
    public MinimumCountIsCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = c => ((ICollection<TItem?>)c!).Count >= count;
        ValidationErrorMessage = MustHaveAMinimumCountOf;
        GetArguments = c => new object?[] { count, ((ICollection<TItem?>)c!).Count };
    }
}
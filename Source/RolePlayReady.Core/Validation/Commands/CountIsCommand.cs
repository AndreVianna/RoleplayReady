namespace System.Validation.Commands;

public sealed class CountIsCommand<TItem>
    : ValidationCommand {
    public CountIsCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = c => ((ICollection<TItem?>)c!).Count == count;
        ValidationErrorMessage = MustHaveACountOf;
        GetArguments = c => new object?[] { count, ((ICollection<TItem?>)c!).Count };
    }
}
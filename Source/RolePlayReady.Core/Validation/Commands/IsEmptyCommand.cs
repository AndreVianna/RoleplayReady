namespace System.Validation.Commands;

public sealed class IsEmptyCommand
    : ValidationCommand {
    public IsEmptyCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Length == 0;
        ValidationErrorMessage = MustBeEmpty;
    }
}

public sealed class IsEmptyCommand<TItem>
    : ValidationCommand {
    public IsEmptyCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = c => ((ICollection<TItem?>)c!).Count == 0;
        ValidationErrorMessage = MustBeEmpty;
    }
}
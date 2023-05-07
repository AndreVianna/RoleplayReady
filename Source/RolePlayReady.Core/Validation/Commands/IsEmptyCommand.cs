namespace System.Validation.Commands;

public sealed class IsEmptyCommand
    : ValidationCommand<string> {
    public IsEmptyCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Length == 0;
        ValidationErrorMessage = MustBeEmpty;
    }
}

public sealed class IsEmptyCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {
    public IsEmptyCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Count == 0;
        ValidationErrorMessage = MustBeEmpty;
    }
}
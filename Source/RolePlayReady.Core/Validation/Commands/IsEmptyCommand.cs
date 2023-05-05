namespace System.Validation.Commands;

public sealed class IsEmptyCommand
    : ValidationCommand<string> {

    public IsEmptyCommand(string subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Length == 0;
        ValidationErrorMessage = MustBeEmpty;
    }
}

public sealed class IsEmpty<TItem>
    : ValidationCommand<ICollection<TItem?>> {

    public IsEmpty(ICollection<TItem?> subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Count == 0;
        ValidationErrorMessage = MustBeEmpty;
    }
}
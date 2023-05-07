namespace System.Validation.Commands;

public sealed class IsOneOfCommand<TValue>
    : ValidationCommand<TValue> {
    public IsOneOfCommand(TValue?[] list, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = list.Contains;
        ValidationErrorMessage = MustBeIn;
        Arguments = SetArguments(list.OfType<object?>().ToArray());
    }
}
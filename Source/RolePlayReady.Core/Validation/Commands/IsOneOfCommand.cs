namespace System.Validation.Commands;

public sealed class IsOneOfCommand<TItem>
    : ValidationCommand {
    public IsOneOfCommand(TItem?[] list, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = i => list.Contains((TItem)i!);
        ValidationErrorMessage = MustBeIn;
        GetArguments = i => new [] { list, i };
    }
}
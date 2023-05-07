namespace System.Validation.Commands;

public sealed class NullCommand : ValidationCommand {
    public NullCommand() : base(string.Empty) { }
}
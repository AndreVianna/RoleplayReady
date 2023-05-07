namespace System.Results;

public sealed record ValidationError {
    public ValidationError(string messageTemplate, string source, params object?[] args) {
        MessageTemplate = Ensure.IsNotNullOrWhiteSpace(messageTemplate);
        Arguments = new object?[args.Length + 1];
        Arguments[0] = Ensure.IsNotNullOrWhiteSpace(source);
        if (args.Length == 0)
            return;
        Array.Copy(args, 0, Arguments, 1, args.Length);
    }

    public string MessageTemplate { get; }
    public object?[] Arguments { get; }
    public string Message => GetErrorMessage(MessageTemplate, Arguments);

    public bool Equals(ValidationError? other)
        => other is not null
           && MessageTemplate == other.MessageTemplate
           && Arguments.SequenceEqual(other.Arguments);

    public override int GetHashCode()
        => Arguments.Aggregate(MessageTemplate.GetHashCode(), HashCode.Combine);
}
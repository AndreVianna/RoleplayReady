namespace System.Results;

public sealed class Nothing : Result {
    public Nothing() { }

    public Nothing(Exception exception) : base(exception) { }

    public static implicit operator Nothing(Success _) => new();
    public static implicit operator Nothing(Exception exception) => new(exception);
}
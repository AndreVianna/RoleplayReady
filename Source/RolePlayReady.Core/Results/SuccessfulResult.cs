namespace System.Results;

public record SuccessfulResult
{
    private SuccessfulResult() { }

    public static SuccessfulResult Success { get; } = new();
}
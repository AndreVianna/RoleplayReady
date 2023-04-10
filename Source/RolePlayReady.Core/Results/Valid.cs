namespace System.Results;

public record Valid
{
    private Valid() { }

    public static Valid Instance { get; } = new();
}
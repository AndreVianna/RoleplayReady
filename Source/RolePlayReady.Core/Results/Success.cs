namespace System.Results;

public record Success
{
    private Success() { }

    public static Success Instance { get; } = new();
}
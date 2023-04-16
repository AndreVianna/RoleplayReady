namespace System.Results;

public record Success
{
    private Success() { }

    public static Success Instance { get; } = new();

    public static bool operator !=(ValidationResult left, Success _) => !left.IsSuccessful;
    public static bool operator ==(ValidationResult left, Success _) => left.IsSuccessful;
}
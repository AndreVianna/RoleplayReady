namespace System.Results;

public record Default<TObject>
{
    private Default() { }

    public static Default<TObject> Instance { get; } = new();

    public TObject? Value { get; } = default;
}
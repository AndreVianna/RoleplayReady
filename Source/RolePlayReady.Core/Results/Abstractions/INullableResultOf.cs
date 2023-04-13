namespace System.Results.Abstractions;

public interface INullableResultOf<out TObject> : IResultOf<TObject> {
    bool IsNull { get; }
    TObject? Default { get; }
}
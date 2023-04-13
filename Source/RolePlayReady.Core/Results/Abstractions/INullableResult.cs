namespace System.Results.Abstractions;

public interface INullableResult<out TObject> : IResult<TObject> {
    bool IsNull { get; }
    TObject? Default { get; }
}
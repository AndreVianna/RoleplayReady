namespace System.Results.Abstractions;

public interface INaybe<out TObject> : IResult<TObject> {
    bool IsNull { get; }
    TObject? Default { get; }
}
namespace System.Results.Abstractions;

public interface IVoidResult {
    bool IsSuccess { get; }
    bool IsException { get; }
    Exception Exception { get; }
    public void Throw();
}

public interface IValidationResult : IVoidResult {
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}

public interface IResult<out TObject> : IValidationResult {
    bool HasValue { get; }
    TObject Value { get; }
}

public interface INullableResult<out TObject> : IResult<TObject> {
    bool IsNull { get; }
    TObject? Default { get; }
}

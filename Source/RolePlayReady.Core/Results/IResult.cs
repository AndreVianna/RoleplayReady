namespace System.Results;

public interface IResult {
    bool IsSuccess { get; }
    bool IsException { get; }

    Exception Exception { get; }
    public void Throw();
}

public interface IValidationResult : IResult {
    public IResult Valid { get; }
}

public interface IResult<out TObject> : IResult {
    bool HasValue { get; }
    bool IsNull { get; }
    bool HasErrors { get; }

    TObject Value { get; }
    TObject? Default { get; }
    ICollection<ValidationError> Errors { get; }
}

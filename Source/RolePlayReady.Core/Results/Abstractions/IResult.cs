namespace System.Results.Abstractions;

public interface IResult {
    bool IsSuccess { get; }
    bool IsException { get; }
    Exception Exception { get; }
    public void Throw();
}

public interface IValidation : IResult {
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}

public interface IResult<out TObject> : IValidation {
    TObject Value { get; }
    TObject? Default { get; }
    bool HasValue { get; }
    bool IsNull { get; }
}

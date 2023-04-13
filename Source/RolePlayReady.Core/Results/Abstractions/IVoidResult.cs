namespace System.Results.Abstractions;

public interface IVoidResult {
    bool IsSuccess { get; }
    bool IsException { get; }
    Exception Exception { get; }
    public void Throw();
}
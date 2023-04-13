namespace System.Results.Abstractions;

public interface IVoidResult : IResult {
    bool IsException { get; }
    Exception Exception { get; }
    public void Throw();
}
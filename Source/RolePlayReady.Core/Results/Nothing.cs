namespace System.Results;

public readonly struct Nothing
{
    private readonly OneOf<Success, Exception> _result;

    public Nothing()
    {
        _result = ResultFactory.Success;
    }

    public Nothing(Exception exception) { _result = exception; }

    public bool IsSuccessful => _result.IsT0;
    public Exception Exception => _result.AsT1;
    public void Throw()
    {
        if (_result.IsT1)
            throw Exception;
    }


    public static implicit operator Nothing(Success _) => new();
    public static implicit operator Nothing(Exception exception) => new(exception);
}
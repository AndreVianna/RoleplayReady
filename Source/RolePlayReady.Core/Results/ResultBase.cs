namespace System.Results;


public abstract record ResultBase : IResult {
    public bool IsSuccessful => Errors.Count == 0;
    public bool HasErrors => Errors.Count != 0;
    public ICollection<ValidationError> Errors { get; protected init; } = new List<ValidationError>();
}
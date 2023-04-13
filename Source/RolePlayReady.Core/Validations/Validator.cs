namespace System.Validations;

public abstract class Validator<TSubject, TChecks, TConnectors>
    : IValidator<TChecks, TConnectors>
    where TSubject : class
    where TChecks : IChecks<TConnectors>
    where TConnectors : IConnectors<TChecks> {

    protected Validator(TSubject? subject, string? source) {
        Subject = subject;
        Source = source!;
        Errors = new();
    }

    protected TSubject? Subject { get; }
    protected string Source { get; }
    protected List<ValidationError> Errors { get; }

    public TChecks And => (TChecks)(IChecks<TConnectors>)this;

    public TConnectors NotNull() {
        if (Subject is null)
            Errors.Add(new(IsRequired, Source));
        return (TConnectors)(IConnectors<TChecks>)this;
    }

    public Validation Result => Errors.Any() ? new(Errors.ToArray()) : new();
}
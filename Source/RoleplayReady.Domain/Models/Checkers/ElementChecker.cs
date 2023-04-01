namespace RoleplayReady.Domain.Models.Checkers;

public abstract record ElementChecker : IElementChecker
{
    protected ElementChecker() { }

    [SetsRequiredMembers]
    protected ElementChecker(Func<IElement, bool> isTrueFor)
    {
        IsTrueFor = isTrueFor;
    }

    public required Func<IElement, bool> IsTrueFor { get; init; }

    public IElementChecker? Next { get; init; }

    public bool Execute(IElement original)
    {
        var result = IsTrueFor(original);
        return result && (Next?.Execute(original) ?? false);
    }
}
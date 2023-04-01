namespace RoleplayReady.Domain.Models.Contracts;

public interface IElementChecker {
    Func<IElement, bool> IsTrueFor { get; init; }

    IElementChecker? Next { get; init; }

    bool Execute(IElement original);
}
namespace RoleplayReady.Domain.Models.Contracts;

public interface IElementModifier {
    Func<IElement, IElement> Modify { get; init; }

    IElementModifier? Next { get; init; }

    IElement Execute(IElement original);
}
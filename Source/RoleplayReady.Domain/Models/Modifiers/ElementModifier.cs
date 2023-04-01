namespace RoleplayReady.Domain.Models.Modifiers;

public abstract record ElementModifier : IElementModifier {
    protected ElementModifier() { }

    [SetsRequiredMembers]
    protected ElementModifier(Func<IElement, IElement> modify) {
        Modify = modify;
    }

    public required Func<IElement, IElement> Modify { get; init; }

    public IElementModifier? Next { get; init; }

    public IElement Execute(IElement original) {
        var result = Modify(original);
        return Next?.Execute(result) ?? result;
    }
}
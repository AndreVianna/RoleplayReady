namespace RoleplayReady.Domain.Models.Effects;

public record Effect : IEffects {
    public Effect() { }

    [SetsRequiredMembers]
    public Effect(Func<IElement, IElement> apply) {
        Apply = apply;
    }

    public required Func<IElement, IElement> Apply { get; init; }
}

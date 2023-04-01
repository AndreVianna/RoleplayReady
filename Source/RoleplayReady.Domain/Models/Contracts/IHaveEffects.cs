namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveEffects {
    public IList<IElementModifier> Effects { get; init; }
}
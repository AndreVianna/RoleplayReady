namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveEffects {
    public IList<IEffects> Effects { get; init; }
}
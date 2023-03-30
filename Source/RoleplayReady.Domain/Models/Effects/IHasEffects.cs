namespace RoleplayReady.Domain.Models.Effects;

public interface IHasEffects {
    IList<Effect> Effects { get; }
}
namespace RoleplayReady.Domain.Models.Effects;

public record Effect {
    public Effect(IHasEffects parent, Func<Actor, Actor> modify) {
        Parent = parent;
        Modify = modify;
    }

    public IHasEffects Parent { get; }
    public Func<Actor, Actor> Modify { get; set; } = _ => _;
}

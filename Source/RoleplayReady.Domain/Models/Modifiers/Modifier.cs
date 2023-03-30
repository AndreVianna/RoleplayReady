namespace RoleplayReady.Domain.Models.Modifiers;

public record Modifier {
    public Modifier(IHasModifiers target, Func<Entity, Entity> modify) {
        Target = target;
        Modify = modify;
    }

    public IHasModifiers Target { get; init; }
    public Func<Entity, Entity> Modify { get; set; } = _ => _;
}
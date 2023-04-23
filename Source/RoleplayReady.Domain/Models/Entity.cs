namespace RolePlayReady.Models;

public abstract record Entity : Base, IEntity {
    public IList<IEntityAttribute> Attributes { get; init; } = new List<IEntityAttribute>();
}

namespace RolePlayReady.Models;

public abstract record Entity : Base, IEntity {
    protected Entity(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<IEntityAttribute> Attributes { get; init; } = new List<IEntityAttribute>();
}

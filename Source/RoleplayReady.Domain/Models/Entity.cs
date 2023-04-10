namespace RolePlayReady.Models;

public abstract record Entity : Base, IEntity {
    protected Entity(IDateTime? dateTime = null) {
        dateTime ??= new DefaultDateTime();
        Timestamp = dateTime.Now;
    }

    public IList<IEntityAttribute> Attributes { get; init; } = new List<IEntityAttribute>();
}

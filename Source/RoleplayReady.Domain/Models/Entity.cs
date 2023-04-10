namespace RolePlayReady.Models;

public abstract record Entity<TKey> : Base<TKey>, IEntity<TKey> {
    protected Entity(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<IEntityAttribute> Attributes { get; init; } = new List<IEntityAttribute>();
}

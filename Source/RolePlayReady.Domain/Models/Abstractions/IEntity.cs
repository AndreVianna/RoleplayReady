namespace RolePlayReady.Models.Abstractions;

public interface IEntity<out TKey> : IBase<TKey> {
    IList<IEntityAttribute> Attributes { get; }
}
namespace RolePlayReady.Models.Abstractions;

public interface IEntity : IBase {
    IList<IEntityAttribute> Attributes { get; }
}
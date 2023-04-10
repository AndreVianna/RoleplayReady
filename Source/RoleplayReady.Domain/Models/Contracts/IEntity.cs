namespace RolePlayReady.Models.Contracts;

public interface IEntity : IBase {
    IList<IEntityAttribute> Attributes { get; }
}
namespace RolePlayReady.Models.Contracts;

public interface IComponent : IEntity {
    IComponent? Root { get; }
    IComponent? Parent { get; set; }
    IList<IComponent> Components { get; }
}

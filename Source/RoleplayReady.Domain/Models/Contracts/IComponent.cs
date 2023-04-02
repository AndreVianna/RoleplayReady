namespace RoleplayReady.Domain.Models.Contracts;

public interface IComponent : IEntity {
    IComponent? Root { get; }
    IComponent? Parent { get; init; }
    IList<IComponent> Components { get; init; }
}

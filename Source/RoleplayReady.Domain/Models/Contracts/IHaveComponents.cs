namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveComponents {
    public IList<IComponent> Components { get; }
}
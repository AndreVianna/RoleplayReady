namespace RoleplayReady.Domain.Models.Contracts;

public interface IRuleSet : IComponent {
    IList<ISource> Sources { get; }
}
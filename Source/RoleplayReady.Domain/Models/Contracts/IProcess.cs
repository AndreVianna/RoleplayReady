namespace RoleplayReady.Domain.Models.Contracts;

public interface IProcess : IComponent {
    IProcessStep Start { get; init; }
}
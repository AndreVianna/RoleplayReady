namespace RoleplayReady.Domain.Models.Contracts;

public interface IProcess : IElement, IHaveAStart {
    IProcessStep Start { get; init; }
}
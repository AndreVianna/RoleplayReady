namespace RoleplayReady.Domain.Models.Contracts;

public interface IProcessStep : IAmKnownAs {
    IProcess Parent { get; init; }
    Func<IEntity, IProcessStep?> Execute { get; init; }
}
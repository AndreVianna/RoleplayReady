namespace RoleplayReady.Domain.Models.Contracts;

public interface IProcessStep : IAmKnownAs, IAmDescribedAs {
    IProcess Parent { get; init; }
    Func<IEntity, IProcessStep?> Execute { get; init; }
}
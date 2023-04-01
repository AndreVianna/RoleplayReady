namespace RoleplayReady.Domain.Models.Contracts;

public interface IRuleSet
    : IEntity,
    IHaveSources,
    IHaveComponents,
    IHavePowerSources,
    IHaveActors,
    IHaveWorkflows {
}
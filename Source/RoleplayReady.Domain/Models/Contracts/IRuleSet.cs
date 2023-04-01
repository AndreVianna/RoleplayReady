namespace RoleplayReady.Domain.Models.Contracts;

public interface IRuleSet 
    : IEntity,
    IHaveSources,
    IHaveComponents,
    IHaveWorkflows {
}
namespace RoleplayReady.Domain.Models.Contracts;

public interface IRuleSet 
    : IEntity,
    IHaveAnAbbreviation,
    IHaveSources,
    IHaveAttributes,
    IHaveElements,
    IHaveWorkflows {
}
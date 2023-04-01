namespace RoleplayReady.Domain.Models.Contracts;

public interface IRuleSet 
    : IEntity,
    IAmAlsoKnownAs,
    IHaveSources,
    IHaveAttributes,
    IHaveElements,
    IHaveWorkflows {
}
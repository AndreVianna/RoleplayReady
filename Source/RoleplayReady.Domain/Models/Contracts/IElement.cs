namespace RoleplayReady.Domain.Models.Contracts;

public interface IElement
    : IEntity,
    ICanBeUsedAs,
    IMayHaveASource,
    IHaveTags,
    IHaveRequirements,
    IHaveElementAttributes,
    IHaveTraits,
    IHaveTriggers,
    IHaveEffects,
    IHaveValidations {
}
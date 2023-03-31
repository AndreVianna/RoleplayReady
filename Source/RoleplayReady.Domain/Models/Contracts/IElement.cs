namespace RoleplayReady.Domain.Models.Contracts;

public interface IElement
    : IChild,
    IHaveStatus,
    ICanBeUsedAs,
    IMayHaveASource,
    IHaveTags,
    IHaveRequirements,
    IHaveElementAttributes,
    IHaveTraits,
    IHavePowerSources,
    IHaveTriggers,
    IHaveEffects,
    IHaveValidations {
}
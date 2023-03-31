namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveTriggers {
    public IList<ITrigger> Triggers { get; init; }
}
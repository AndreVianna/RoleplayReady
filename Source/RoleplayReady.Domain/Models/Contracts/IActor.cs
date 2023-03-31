namespace RoleplayReady.Domain.Models.Contracts;

public interface IActor : IHavePowers {
    IList<IBundle> Possessions { get; init; }
    IList<IAction> Actions { get; init; }
    IList<ICondition> Conditions { get; init; }
    IList<IEntry> Journal { get; init; }
}
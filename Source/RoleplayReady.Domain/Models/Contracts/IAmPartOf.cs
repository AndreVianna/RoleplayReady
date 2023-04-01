namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmPartOf {
    IRuleSet? RuleSet { get; init; }
}
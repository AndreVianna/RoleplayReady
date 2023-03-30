namespace RoleplayReady.Domain.Models;

public interface IAttribute
{
    RuleSet RuleSet { get; }
    string Name { get; }
    string? Description { get; }
    Type Type { get; }
}
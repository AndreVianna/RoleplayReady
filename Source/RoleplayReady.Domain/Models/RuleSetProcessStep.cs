namespace RoleplayReady.Domain.Models;

public record RuleSetProcessStep
{
    // SystemProcess and Order must be unique.
    public required RuleSetProcess RuleSetProcess { get; init; }
    public required int Order { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
namespace RoleplayReady.Domain.Models;

public record RuleSetProcess
{
    // RuleSet and Process must be unique.
    public required RuleSet RuleSet { get; init; }
    public required Process Process { get; init; }
    public IList<RuleSetProcessStep> Steps { get; init; } = new List<RuleSetProcessStep>();
}
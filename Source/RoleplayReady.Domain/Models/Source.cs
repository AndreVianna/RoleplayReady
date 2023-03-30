namespace RoleplayReady.Domain.Models;

public class Source
{
    // RuleSet and Abbreviation must be unique.
    public required RuleSet RuleSet { get; init; }
    public required string OwnerId { get; init; }
    public required string Name { get; init; }
    public required string Abbreviation { get; init; }
    public string? Description { get; init; }
    public string? Publisher { get; init; }
}

namespace RoleplayReady.Domain.Models;

public record Element : IHasTags, IHasRequirements, IHasValidations {
    // RuleSet, OwnerId, and Name must be unique.
    public required RuleSet RuleSet { get; init; }
    public required string OwnerId { get; init; }
    public required string Name { get; init; }
    public IList<string> Tags { get; set; } = new List<string>();

    public required ElementType Type { get; init; }
    public Usage Usage { get; init; }
    public required Source Source { get; init; }

    public string? Description { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public Status Status { get; init; }

    public IList<Validation> Requirements { get; set; } = new List<Validation>();

    public IList<Validation> Validations { get; set; } = new List<Validation>();
}
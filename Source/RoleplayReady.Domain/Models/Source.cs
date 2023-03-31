namespace RoleplayReady.Domain.Models;

public record Source : ISource {
    public Source() {

    }

    [SetsRequiredMembers]
    public Source(string abbreviation, string name, string? description = null, string? publisher = null) {
        Abbreviation = abbreviation;
        Name = name;
        Description = description;
        Publisher = publisher;
    }

    // Abbreviation must be unique.
    public required string Abbreviation { get; init; }
    // Name must be unique.
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? Publisher { get; init; }
}

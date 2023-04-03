using RolePlayReady.Models.Contracts;
using RolePlayReady.Utilities;

namespace RolePlayReady.Models;

public record Source : ISource {
    public Source() {
    }

    [SetsRequiredMembers]
    public Source(string abbreviation, string name, string description, string? publisher = null) {
        Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Publisher = publisher;
    }

    [SetsRequiredMembers]
    public Source(string name, string description, string? publisher = null)
        : this(name.ToAcronym(), name, description, publisher) {
    }

    // Abbreviation must be unique.
    public required string Abbreviation { get; init; }
    // Name must be unique.
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? Publisher { get; init; }
}

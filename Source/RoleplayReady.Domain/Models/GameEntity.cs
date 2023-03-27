namespace RoleplayReady.Domain.Models;

public record GameEntity
{
    public required int Id { get; init; }
    public required GameSystem GameSystem { get; init; }
    public required string OwnerId { get; init; }
    public EntityType EntityType { get; init; }
    public required string Name { get; init; }
    // GameSystem, OwnerId, Name, must be unique.

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; }

    public IReadOnlyList<EntityProperty> Properties { get; init; } = new List<EntityProperty>();
    public IReadOnlyList<Item> Items { get; init; } = new List<Item>();
    public IReadOnlyList<Spell> Spells { get; init; } = new List<Spell>();
    public IReadOnlyList<Modifier> Modifiers { get; init; } = new List<Modifier>();
    public IReadOnlyList<Entry> Entries { get; init; } = new List<Entry>();
}
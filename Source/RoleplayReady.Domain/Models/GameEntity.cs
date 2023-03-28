using System.Reflection.Metadata;

namespace RoleplayReady.Domain.Models;

public record GameEntity
{
    public required GameSystem System { get; init; }
    public required string OwnerId { get; init; }
    public EntityType Type { get; init; }
    public required string Name { get; init; }
    // GameSystem, OwnerId, Name, must be unique.

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; }

    public IList<Set> Equipment { get; init; } = new List<Set>();
    public IList<Power> Powers { get; init; } = new List<Power>();
    public IList<EntityAction> Actions { get; init; } = new List<EntityAction>();
    public IList<Condition> Conditions { get; init; } = new List<Condition>();
    public IList<IEntityProperty> Properties { get; init; } = new List<IEntityProperty>();

    public IList<Modifier> Modifiers { get; init; } = new List<Modifier>();
    public IList<Entry> Entries { get; init; } = new List<Entry>();
}
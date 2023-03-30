namespace RoleplayReady.Domain.Models;

public record MagicSource
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required string SpellcastingAbility { get; init; }
    public int CantripsKnown { get; init; }
    public int SpellsKnown { get; init; }
    public int MaximumSlotLevel { get; init; }
    public IDictionary<int, int> SlotsPerLevel { get; } = new Dictionary<int, int>();
    public IList<string> SpellList { get; } = new List<string>();
}

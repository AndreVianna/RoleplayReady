namespace RoleplayReady.Domain.Models;

public record Class
{
    public required string Name { get; init; }
    public required int Level { get; init; }

    public required string SpellcastingAbility { get; init; }
    public required int[] SpellSlots { get; init; }
    public required string[] SpellsKnown { get; init; }
    public required int SpellSaveDC { get; init; }

    public required int HitDiceType { get; init; }
}

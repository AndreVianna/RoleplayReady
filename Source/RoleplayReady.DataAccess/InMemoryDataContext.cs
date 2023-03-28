using RoleplayReady.Domain.Models;

namespace RoleplayReady.DataAccess;
public class InMemoryDataContext
{
    public Dictionary<string, GameSystem> Systems { get; } = new();
    public Dictionary<string, ElementType> ElementTypes { get; } = new();

    public void AddDnD5e()
    {
        // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
        var dnd5e = new GameSystem
        {
            OwnerId = "System",
            Name = "Dungeons & Dragons 5th Edition",
            Description = "Dungeons & Dragons 5th Edition is a tabletop role-playing game system.",
            Abbreviation = "DnD5e",
            Publisher = "Wizards of the Coast",
            Status = SystemStatus.Public,
        };
        Systems.Add(dnd5e.Abbreviation, dnd5e);

        var raceElementType = new ElementType
        {
            System = dnd5e,
            Name = "Race",
            Description = "Represents the character's race in Dungeons & Dragons 5th Edition.",
        };

        dnd5e.ElementTypes.Add(raceElementType);
    }

}

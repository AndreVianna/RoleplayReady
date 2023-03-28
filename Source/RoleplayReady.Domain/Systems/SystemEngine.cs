namespace RoleplayReady.Domain.Systems;

public static class SystemEngine
{
    private static Dictionary<string, ElementType> _elementTypes = new Dictionary<string, ElementType>();
    // ReSharper disable once InconsistentNaming - Dnd5e is the official name of the system.

    public static void CreateElementTypes()
    {
        var raceElementType = new ElementType
        {
            Name = "Race",
            Description = "Represents the character's race in Dungeons & Dragons 5th Edition.",
        };

        var result = new GameSystem
        {
            OwnerId = "System",
            Name = "Dungeons & Dragons 5th Edition",
            Description = "Dungeons & Dragons 5th Edition is a tabletop role-playing game system.",
            Abbreviation = "DnD5e",
            Publisher = "Wizards of the Coast",
            Status = SystemStatus.Public,
        };
        result.ElementTypes.Add(raceElementType);
        return result;
    }

    public static GameSystem CreateDnD5eSystem()
    {
        var raceElementType = new ElementType
        {
            Name = "Race",
            Description = "Represents the character's race in Dungeons & Dragons 5th Edition.",
        };

        var result = new GameSystem
        {
            OwnerId = "System",
            Name = "Dungeons & Dragons 5th Edition",
            Description = "Dungeons & Dragons 5th Edition is a tabletop role-playing game system.",
            Abbreviation = "DnD5e",
            Publisher = "Wizards of the Coast",
            Status = SystemStatus.Public,
        };
        result.ElementTypes.Add(raceElementType);
        return result;
    }

    public static Element CreateDragonbornRace(GameSystem system)
    {
        var dragonbornRace = new Element
        {
            System = system,
            OwnerId = "core",
            Name = "Dragonborn",
            Type = raceElementType,
            Description = "Born of dragons, as their name proclaims, the dragonborn walk proudly through a world that greets them with fearful incomprehension.",
            Availability = Availability.Standard,
        };

        // Add the Dragonborn race to the list of elements in the DnD 5e system
        dnd5eSystem.Elements.Add(dragonbornRace);
    }

    private static Entity CreateEntity<T>(this T system, string ownerId, Source source, ElementType elementType, string name) where T : Models.GameSystem
    {
        var entity = new Entity
        {
            Name = name,
            OwnerId = ownerId,
            System = system,
            Type = elementType,
            Source = source,
        };
        var modifier = system.Modifiers.First(i => i.Name == "SetEntity");
        if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
        modifier = system.Modifiers.First(i => i.Name == $"Set{elementType}");
        if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
        return entity;
    }

    public static Entity ChooseRace<T>(this T system, Entity entity, string race) where T : Models.GameSystem
    {
        switch (race)
        {
            case "Elf":
                var modifier = system.Modifiers.First(i => i.Name == "SetElfRace");
                if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
                modifier = system.Modifiers.First(i => i.Name == "SetElfAbilityScoreChange");
                if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
                modifier = system.Modifiers.First(i => i.Name == $"SetElfSpeed");
                if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
                modifier = system.Modifiers.First(i => i.Name == $"SetElfLifespan");
                if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
                modifier = system.Modifiers.First(i => i.Name == $"SetElfTrance");
                if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
                modifier = system.Modifiers.First(i => i.Name == $"AddElfSubrace");
                if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
                break;
        }
        return entity;
    }
}
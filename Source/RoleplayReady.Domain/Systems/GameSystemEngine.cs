namespace RoleplayReady.Domain.Systems;

public static class GameSystemEngine
{
    private static Entity CreateEntity<T>(this T system, string ownerId, ElementType elementType, string name) where T : Models.System
    {
        var entity = new Entity
        {
            Name = name,
            OwnerId = ownerId,
            System = system,
            Type = elementType,
        };
        var modifier = system.Modifiers.First(i => i.Name == "SetEntity");
        if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
        modifier = system.Modifiers.First(i => i.Name == $"Set{elementType}");
        if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
        return entity;
    }

    public static Entity ChooseRace<T>(this T system, Entity entity, string race) where T : Models.System
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
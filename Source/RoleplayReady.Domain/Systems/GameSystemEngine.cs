namespace RoleplayReady.Domain.Systems;

public static class GameSystemEngine
{
    private static GameEntity CreateEntity<T>(this T system, string ownerId, EntityType entityType, string name) where T : GameSystem
    {
        var entity = new GameEntity
        {
            Name = name,
            OwnerId = ownerId,
            System = system,
            Type = entityType,
        };
        var modifier = system.Modifiers.First(i => i.Name == "SetEntity");
        if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
        modifier = system.Modifiers.First(i => i.Name == $"Set{entityType}");
        if (modifier.IsApplicable(entity)) entity.Modifiers.Add(modifier);
        return entity;
    }

    public static GameEntity ChooseRace<T>(this T system, GameEntity entity, string race) where T : GameSystem
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
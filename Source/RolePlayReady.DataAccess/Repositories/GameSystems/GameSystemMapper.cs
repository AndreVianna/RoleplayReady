namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public static class GameSystemMapper {
    public static GameSystemData ToData(GameSystem input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
        };

    public static Row ToRow(GameSystemData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static GameSystem? ToModel(GameSystemData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                ShortName = input.ShortName,
                Name = input.Name,
                Description = input.Description,
                Tags = input.Tags,
            };
}

namespace RolePlayReady.DataAccess.Repositories.GameSystems;

internal static class GameSystemMapper {
    public static GameSystemData Map(this GameSystem input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
        };

    public static Row MapToRow(this GameSystemData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static GameSystem? Map(this GameSystemData? input)
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

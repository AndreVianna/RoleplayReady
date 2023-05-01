namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemMapper : IDataMapper<GameSystem, Row, GameSystemData> {
    public GameSystemData ToData(GameSystem input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
        };

    public Row ToRow(GameSystemData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public GameSystem? ToModel(GameSystemData? input)
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

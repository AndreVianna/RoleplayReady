using DataModel = RolePlayReady.DataAccess.Repositories.GameSystems.GameSystemDataModel;

namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public static class GameSystemMapper {
    public static DataModel Map(this IGameSystem input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
        };

    public static GameSystem Map(this IDataFile<DataModel> input)
        => new() {
            Id = Guid.Parse(input.Name),
            ShortName = input.Content.ShortName,
            Name = input.Content.Name,
            Description = input.Content.Description,
            Timestamp = input.Timestamp,
            State = State.Pending,
            Tags = input.Content.Tags,
        };
}

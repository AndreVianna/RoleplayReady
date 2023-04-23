namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public static class GameSystemMapper {
    public static GameSystemData Map(this GameSystem input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            Domains = input.Domains.Select(x => x.Name).ToArray(),
        };

    public static Row MapToRow(this Persisted<GameSystemData> input)
        => new() {
            Id = input.Id,
            Name = input.Content.Name,
        };

    public static Persisted<GameSystem>? Map(this Persisted<GameSystemData>? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                Timestamp = input.Timestamp,
                State = State.Pending,
                Content = new() {
                    ShortName = input.Content.ShortName,
                    Name = input.Content.Name,
                    Description = input.Content.Description,
                    Tags = input.Content.Tags,
                },
            };
}

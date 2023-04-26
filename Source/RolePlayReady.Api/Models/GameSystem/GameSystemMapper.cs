using DomainGameSystem = RolePlayReady.Models.GameSystem;

namespace RolePlayReady.Api.Models.GameSystem;

internal static class GameSystemMapper {
    public static GameSystemRowModel[] ToResponse(this IEnumerable<Row> rows)
        => rows.Select(ToResponse).ToArray();

    private static GameSystemRowModel ToResponse(this Row row)
        => new() {
            Id = row.Id,
            Name = row.Name
        };

    public static GameSystemModel ToResponse(this DomainGameSystem model)
        => new() {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            ShortName = model.ShortName,
            Tags = model.Tags,
        };

    public static DomainGameSystem ToDomain(this GameSystemRequest request, Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ShortName = request.ShortName,
            Tags = request.Tags,
        };
}

using DomainGameSystem = RolePlayReady.Models.GameSystem;

namespace RolePlayReady.Api.Controllers.GameSystem.Models;

internal static class GameSystemMapper {
    public static GameSystemRowResponse[] ToResponse(this IEnumerable<Row> rows)
        => rows.Select(ToResponse).ToArray();

    private static GameSystemRowResponse ToResponse(this Row row)
        => new() {
            Id = (Base64Guid)row.Id,
            Name = row.Name
        };

    public static GameSystemResponse ToResponse(this DomainGameSystem model)
        => new() {
            Id = (Base64Guid)model.Id,
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

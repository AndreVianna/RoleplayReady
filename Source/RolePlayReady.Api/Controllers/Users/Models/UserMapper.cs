namespace RolePlayReady.Api.Controllers.Users.Models;

internal static class UserMapper {
    public static UserRowResponse[] ToResponse(this IEnumerable<UserRow> rows)
        => rows.Select(ToResponse).ToArray();

    private static UserRowResponse ToResponse(this UserRow row)
        => new() {
            Id = (Base64Guid)row.Id,
            Email = row.Email,
            Name = row.Name
        };

    public static UserResponse ToResponse(this User model, DateTime now)
        => new() {
            Id = (Base64Guid)model.Id,
            Email = model.Email,
            IsLocked = model.LockExpiration > now,
            IsBlocked = model.IsBlocked,
            Roles = model.Roles.Select(x => x.ToString()).ToArray(),
            FolderName = model.FolderName,
            Name = model.Name,
            Birthday = model.Birthday,
        };

    public static User ToDomain(this UserRequest request, Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Birthday = request.Birthday,
        };
}

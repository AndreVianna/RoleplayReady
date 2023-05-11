using RolePlayReady.Handlers.Auth;

namespace RolePlayReady.DataAccess.Repositories.Users;

public static class UserMapper {
    public static UserData ToData(this User input)
        => new() {
            Id = input.Id,
            Email = input.Email,
            HashedPassword = input.HashedPassword,
            LockExpiration = input.LockExpiration,
            SignInRetryCount = input.SignInRetryCount,
            IsBlocked = input.IsBlocked,
            Roles = input.Roles,
            Name = input.Name,
            Birthday = input.Birthday,
        };

    public static UserRow ToRow(this UserData input)
        => new() {
            Id = input.Id,
            Email = input.Email,
            Name = input.Name ?? string.Empty,
        };

    public static User? ToModel(this UserData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                Email = input.Email,
                HashedPassword = input.HashedPassword,
                LockExpiration = input.LockExpiration,
                SignInRetryCount = input.SignInRetryCount,
                IsBlocked = input.IsBlocked,
                Roles = input.Roles,
                Name = input.Name,
                Birthday = input.Birthday,
            };
}

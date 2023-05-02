namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserMapper : IUserMapper {
    public UserData ToData(User input)
        => new() {
            Id = input.Id,
            Email = input.Email,
            LockExpiration = input.LockExpiration,
            SignInRetryCount = input.SignInRetryCount,
            IsBlocked = input.IsBlocked,
            Roles = input.Roles,
            Name = input.Name,
            Birthday = input.Birthday,
        };

    public UserRow ToRow(UserData input)
        => new() {
            Id = input.Id,
            Email = input.Email,
            Name = input.Name ?? string.Empty,
        };

    public User? ToModel(UserData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                Email = input.Email,
                LockExpiration = input.LockExpiration,
                SignInRetryCount = input.SignInRetryCount,
                IsBlocked = input.IsBlocked,
                Roles = input.Roles,
                Name = input.Name,
                Birthday = input.Birthday,
            };
}

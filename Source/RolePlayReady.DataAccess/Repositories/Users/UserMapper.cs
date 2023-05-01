namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserMapper : IDataMapper<User, UserRow, UserData> {
    public UserData ToData(User input)
        => new() {
            Id = input.Id,
            Username = input.Username,
            Email = input.Email,
            PasswordHash = input.PasswordHash,
            PasswordSalt = input.PasswordSalt,
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
            Name = input.Name,
        };

    public User? ToModel(UserData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                Username = input.Username,
                Email = input.Email,
                PasswordHash = input.PasswordHash,
                PasswordSalt = input.PasswordSalt,
                LockExpiration = input.LockExpiration,
                SignInRetryCount = input.SignInRetryCount,
                IsBlocked = input.IsBlocked,
                Roles = input.Roles,
                Name = input.Name,
                Birthday = input.Birthday,
            };
}

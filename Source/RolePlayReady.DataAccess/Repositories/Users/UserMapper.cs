namespace RolePlayReady.DataAccess.Repositories.Users;

internal static class UserMapper {
    public static UserData Map(this User input)
        => new() {
            Id= input.Id,
            Username= input.Username,
            Email= input.Email,
            PasswordHash= input.PasswordHash,
            PasswordSalt= input.PasswordSalt,
            LockExpiration= input.LockExpiration,
            SignInRetryCount= input.SignInRetryCount,
            IsBlocked= input.IsBlocked,
            Roles= input.Roles,
            Name= input.Name,
            Birthday= input.Birthday,
        };

    public static UserRow MapToRow(this UserData input)
        => new() {
            Id = input.Id,
            Email = input.Email,
            Name = input.Name,
        };

    public static User Map(this UserData input)
        => new() {
            Id= input.Id,
            Username= input.Username,
            Email= input.Email,
            PasswordHash= input.PasswordHash,
            PasswordSalt= input.PasswordSalt,
            LockExpiration= input.LockExpiration,
            SignInRetryCount= input.SignInRetryCount,
            IsBlocked= input.IsBlocked,
            Roles= input.Roles,
            Name= input.Name,
            Birthday= input.Birthday,
        };
}

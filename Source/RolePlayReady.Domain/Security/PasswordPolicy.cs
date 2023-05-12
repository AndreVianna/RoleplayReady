using System.Security;

namespace RolePlayReady.Security;

public class PasswordPolicy : IPasswordPolicy {
    public ValidationResult Enforce(string password) => ValidationResult.Success();
}
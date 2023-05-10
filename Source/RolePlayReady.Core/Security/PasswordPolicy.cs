namespace System.Security;

public abstract class PasswordPolicy : IPasswordPolicy {
    public bool TryValidate(string password, out ValidationResult result) {
        result = ValidationResult.Success();
        return true;
    }
}
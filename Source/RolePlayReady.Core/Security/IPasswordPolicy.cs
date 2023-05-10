namespace System.Security;

public interface IPasswordPolicy {
    bool TryValidate(string password, out ValidationResult result);
}
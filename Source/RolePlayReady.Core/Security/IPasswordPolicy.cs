namespace System.Security;

public interface IPasswordPolicy {
    ValidationResult Enforce(string password);
}
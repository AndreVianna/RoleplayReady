namespace System.Security;

public abstract class PasswordPolicy : IPasswordPolicy {
    public bool TryValidate(string password, out ICollection<ValidationError> errors) {
        errors = new List<ValidationError>();
        return true;
    }
}
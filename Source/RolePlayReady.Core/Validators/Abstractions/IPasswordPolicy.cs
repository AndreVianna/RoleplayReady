namespace System.Validators.Abstractions;

public interface IPasswordPolicy {
    bool TryValidate(string password, out ICollection<ValidationError> errors);
}
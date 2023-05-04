namespace System.Validations.Abstractions;

public interface IValidatable {
    ICollection<ValidationError> Validate();
}

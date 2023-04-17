namespace System.Validations;

public class ObjectValidation
    : Validation<object, IObjectValidation>,
      IObjectValidation {

    public ObjectValidation(object? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }
}
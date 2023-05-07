namespace System.Validation.Builder;

public class ObjectValidators : Validators<object?>, IObjectValidators {
    public ObjectValidators(object? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
    }
}
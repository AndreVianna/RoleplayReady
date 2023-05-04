namespace System.Validations;

public class ValidatableValidations : IValidatableValidations {

    public ValidatableValidations(IValidatable? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
    }

    public IValidatable? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public ICollection<ValidationError> IsValid() {
        if (Subject is null) return Errors;
        foreach (var error in Subject.Validate()) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            Errors.Add(error);
        }

        return Errors;
    }
}
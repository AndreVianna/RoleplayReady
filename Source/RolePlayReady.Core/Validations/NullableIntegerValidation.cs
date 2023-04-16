namespace System.Validations;

public class NullableIntegerValidation
    : Validation<int?, IIntegerValidation>,
      INullableIntegerValidation {

    public NullableIntegerValidation(int? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IIntegerValidation NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return new IntegerValidation(Subject, Source, Errors);
    }
}
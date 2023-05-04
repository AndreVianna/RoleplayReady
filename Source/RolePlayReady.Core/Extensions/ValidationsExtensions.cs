namespace System.Extensions;

public static class ValidationsExtensions {
    public static IValidationsConnector<TSubject, TValidation> AsConnection<TSubject, TValidation>(this TValidation validation)
        where TValidation : Validations<TSubject, TValidation>
        => new ValidationsConnector<TSubject, TValidation>(validation.Subject, validation);
}
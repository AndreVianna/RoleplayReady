using System.Validation.Builder;

namespace System.Extensions;

public static class ValidatorsExtensions {
    public static IConnectors<TSubject, TValidator> AsConnection<TSubject, TValidator>(this TValidator validation)
        where TValidator : Validators<TSubject, TValidator>
        => new Connectors<TSubject, TValidator>(validation.Subject, validation);
}
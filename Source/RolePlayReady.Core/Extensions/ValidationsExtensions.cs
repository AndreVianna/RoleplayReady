using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class ValidatorsExtensions {
    public static IValidatorsConnector<TSubject, TValidator> AsConnection<TSubject, TValidator>(this TValidator validation)
        where TValidator : Validators<TSubject, TValidator>
        => new Connectors<TSubject, TValidator>(validation.Subject, validation);
}
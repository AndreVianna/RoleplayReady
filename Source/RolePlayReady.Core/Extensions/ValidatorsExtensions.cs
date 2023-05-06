namespace System.Extensions;

public static class ValidatorsExtensions {
    public static IConnectors<TSubject, TValidator> AsConnection<TSubject, TValidator>(this TValidator validation)
        where TValidator : Validators<TSubject>
        => new Connectors<TSubject, TValidator>(validation);
}
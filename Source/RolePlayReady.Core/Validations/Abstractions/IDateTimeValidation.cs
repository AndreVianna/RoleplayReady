namespace System.Validations.Abstractions;

public interface IDateTimeValidation
    : IConnectsToOrFinishes<IDateTimeValidation> {
    IConnectsToOrFinishes<IDateTimeValidation> After(DateTime reference);
    IConnectsToOrFinishes<IDateTimeValidation> Before(DateTime reference);
    IConnectsToOrFinishes<IDateTimeValidation> StartsOn(DateTime reference);
    IConnectsToOrFinishes<IDateTimeValidation> EndsOn(DateTime reference);
}
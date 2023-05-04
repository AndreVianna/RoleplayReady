namespace System.Validations.Abstractions;

public interface IDateTimeValidations : IValidations<DateTime?> {
    IConnects<IDateTimeValidations> IsAfter(DateTime reference);
    IConnects<IDateTimeValidations> IsBefore(DateTime reference);
    IConnects<IDateTimeValidations> StartsOn(DateTime reference);
    IConnects<IDateTimeValidations> EndsOn(DateTime reference);
}
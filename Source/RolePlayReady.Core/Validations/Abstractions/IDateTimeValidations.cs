namespace System.Validations.Abstractions;

public interface IDateTimeValidations : IValidations<DateTime?, DateTimeValidations> {
    IValidationsConnector<DateTime?, DateTimeValidations> IsAfter(DateTime reference);
    IValidationsConnector<DateTime?, DateTimeValidations> IsBefore(DateTime reference);
    IValidationsConnector<DateTime?, DateTimeValidations> StartsOn(DateTime reference);
    IValidationsConnector<DateTime?, DateTimeValidations> EndsOn(DateTime reference);
}
namespace System.Validation.Builder.Abstractions;

public interface IDateTimeValidator : IValidator {
    IConnector<DateTimeValidator> IsNull();
    IConnector<DateTimeValidator> IsNotNull();
    IConnector<DateTimeValidator> IsAfter(DateTime reference);
    IConnector<DateTimeValidator> IsBefore(DateTime reference);
    IConnector<DateTimeValidator> StartsOn(DateTime reference);
    IConnector<DateTimeValidator> EndsOn(DateTime reference);
}
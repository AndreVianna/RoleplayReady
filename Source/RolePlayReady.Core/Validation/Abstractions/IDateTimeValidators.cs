namespace System.Validation.Abstractions;

public interface IDateTimeValidators : IValidators<DateTime?, DateTimeValidators> {
    IValidatorsConnector<DateTime?, DateTimeValidators> IsAfter(DateTime reference);
    IValidatorsConnector<DateTime?, DateTimeValidators> IsBefore(DateTime reference);
    IValidatorsConnector<DateTime?, DateTimeValidators> StartsOn(DateTime reference);
    IValidatorsConnector<DateTime?, DateTimeValidators> EndsOn(DateTime reference);
}
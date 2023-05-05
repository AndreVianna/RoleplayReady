namespace System.Validation.Builder.Abstractions;

public interface IDateTimeValidators : IValidators<DateTime?, DateTimeValidators> {
    IConnectors<DateTime?, DateTimeValidators> IsAfter(DateTime reference);
    IConnectors<DateTime?, DateTimeValidators> IsBefore(DateTime reference);
    IConnectors<DateTime?, DateTimeValidators> StartsOn(DateTime reference);
    IConnectors<DateTime?, DateTimeValidators> EndsOn(DateTime reference);
}
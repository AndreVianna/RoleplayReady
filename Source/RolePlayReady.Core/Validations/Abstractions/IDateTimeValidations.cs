namespace System.Validations.Abstractions;

public interface IDateTimeValidations : IValidations {
    public IConnectors<IDateTimeValidations> After(DateTime reference);
    public IConnectors<IDateTimeValidations> Before(DateTime minimumLength);
    public IConnectors<IDateTimeValidations> AtOrAftter(DateTime reference);
    public IConnectors<IDateTimeValidations> AtOrBefore(DateTime reference);
}
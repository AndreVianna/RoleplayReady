namespace System.Validations.Abstractions;

public interface IStringChecks : IChecks<IStringConnectors> {
    IStringConnectors NotEmptyOrWhiteSpace();
    IStringConnectors NoLongerThan(int maximumLength);
}
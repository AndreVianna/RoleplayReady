namespace System.Validations.Abstractions;

public interface IStringsChecks : IChecks<IStringsConnectors> {
    IStringsConnectors EachItemIs(Func<StringValidator, IStringConnectors> validate);
}
namespace System.Validations.Abstractions;

public interface IStringsChecks : IChecks<IStringsConnectors> {
    IStringsConnectors ItemsAre(Func<StringValidator, IStringConnectors> validate);
}
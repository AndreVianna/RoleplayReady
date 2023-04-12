namespace System.Validations.Abstractions;

public interface IChecks<out TConnectors> {
    TConnectors NotNull();
}
namespace System.Validations.Abstractions;

public interface IValidator { }

public interface IValidator<out TChecks, out TConnectors>
    : IValidator,
        IChecks<TConnectors>,
        IConnectors<TChecks>
    where TChecks : IChecks<TConnectors>
    where TConnectors : IConnectors<TChecks> {
}


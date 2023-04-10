using System.Results;

namespace System.Validations;

public abstract class Validatable : IValidatable {
    public virtual ValidationResult Validate<TContext>(TContext? context = null)
        where TContext : class
        => ValidationResult.Valid;
}
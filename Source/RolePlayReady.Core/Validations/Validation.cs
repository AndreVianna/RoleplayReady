namespace RolePlayReady.Validations;

public abstract record Validatable : IValidatable {
    public virtual ValidationResult Validate<TContext>(TContext? context = null)
        where TContext : class
        => ValidationResult.Valid;
}
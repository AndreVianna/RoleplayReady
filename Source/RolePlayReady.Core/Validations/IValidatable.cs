namespace RolePlayReady.Validations;

public interface IValidatable {
    ValidationResult Validate<TContext>(TContext? context = null)
        where TContext : class;
}
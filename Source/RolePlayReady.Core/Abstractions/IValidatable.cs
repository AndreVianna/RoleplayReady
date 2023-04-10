using System.Results;

namespace System.Abstractions;

public interface IValidatable {
    ValidationResult Validate<TContext>(TContext? context = null)
        where TContext : class;
}
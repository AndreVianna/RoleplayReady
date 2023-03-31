namespace RoleplayReady.Domain.Models.Validations;

public interface IRequirement {
    Func<IElement, ValidationResult> Check { get; }
}
namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveValidations {
    public IList<IValidation> Validations { get; init; }
}

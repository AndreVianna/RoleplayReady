namespace RoleplayReady.Domain.Models.Validations;

public interface IHasValidations {
    IList<Validation> Validations { get; }
}

namespace RoleplayReady.Domain.Models;

public interface IHasValidations
{
    IList<EntityValidation> Validations { get; init; }
}

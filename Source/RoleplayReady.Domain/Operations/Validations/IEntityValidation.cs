using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Operations.Validations;

public interface IEntityValidation
    : IEntityOperation<IEntityValidation, IList<ValidationError>> {
    Func<IEntity, IList<ValidationError>, IList<ValidationError>> Assert { get; init; }
}

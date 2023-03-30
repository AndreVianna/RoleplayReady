namespace RoleplayReady.Domain.Models.Validations;

public interface IHasRequirements {
    IList<Validation> Requirements { get; }
}
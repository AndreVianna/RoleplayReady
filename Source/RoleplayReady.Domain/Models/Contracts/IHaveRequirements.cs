namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveRequirements {
    public IList<IValidation> Requirements { get; init; }
}
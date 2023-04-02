namespace RoleplayReady.Domain.Models.Contracts;

public interface IValidation {
    Func<IEntity, bool> Validate { get; init; }
    string FailureMessage { get; init; }
}
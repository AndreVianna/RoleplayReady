namespace RoleplayReady.Domain.Models.Contracts;

public interface IValidation {
    Func<IElement, bool> Validate { get; init; }
    string FailureMessage { get; init; }
}
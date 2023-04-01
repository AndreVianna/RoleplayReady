namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmApplicable {
    Func<IElement, IElement> Apply { get; init; }
}
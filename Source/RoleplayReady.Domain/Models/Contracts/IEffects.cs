namespace RoleplayReady.Domain.Models.Contracts;

public interface IEffects {
    Func<IElement, IElement> Apply { get; init; }
}
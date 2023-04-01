namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveElements {
    public IList<IElement> Elements { get; }
}
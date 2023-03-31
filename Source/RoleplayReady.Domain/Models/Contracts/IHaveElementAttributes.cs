namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveElementAttributes {
    public IList<IElementAttribute> Attributes { get; init; }
}
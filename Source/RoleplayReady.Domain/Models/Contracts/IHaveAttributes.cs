namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveAttributes {
    public IList<IAttribute> Attributes { get; init; }
}
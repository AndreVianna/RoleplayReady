namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveAttribute {
    public IAttribute Attribute { get; init; }
}
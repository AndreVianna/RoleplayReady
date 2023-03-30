namespace RoleplayReady.Domain.Models;

public interface IHasAttributes {
    IList<IAttributeWithValue> Attributes { get; }
}
namespace RoleplayReady.Domain.Models;

public record Concept : Element, IHasAttributes, IHasFeatures {
    public IList<IAttributeWithValue> Attributes { get; init; } = new List<IAttributeWithValue>();
    public IList<Feature> Features { get; init; } = new List<Feature>();
}
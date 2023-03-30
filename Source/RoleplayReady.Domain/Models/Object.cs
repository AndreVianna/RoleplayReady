namespace RoleplayReady.Domain.Models;

public record Object : Element, IHasFeatures {
    public IList<Feature> Features { get; init; } = new List<Feature>();
}
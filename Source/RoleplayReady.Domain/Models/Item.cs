namespace RoleplayReady.Domain.Models;

public record Item : Element, IHasFeatures
{
    public IList<Feature> Features { get; init; } = new List<Feature>();
}
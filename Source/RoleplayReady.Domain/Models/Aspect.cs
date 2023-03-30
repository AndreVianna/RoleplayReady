namespace RoleplayReady.Domain.Models;

public record Aspect : Element, IHasAttributes, IHasFeatures
{
    public IList<IElementAttribute> Attributes { get; init; } = new List<IElementAttribute>();
    public IList<Feature> Features { get; init; } = new List<Feature>();
}
namespace RoleplayReady.Domain.Models;

public record PowerSource : Element, IHasAttributes, IHasRequirements, IHasPowers {
    public required string Category { get; set; }
    public required string ResourceType { get; set; }
    public required string UnitType { get; set; }
    public IList<IAttributeWithValue> Attributes { get; } = new List<IAttributeWithValue>();
    public IList<Power> Powers { get; } = new List<Power>();
}
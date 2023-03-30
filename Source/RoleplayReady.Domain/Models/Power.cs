namespace RoleplayReady.Domain.Models;

public record Power : Element, IHasAttributes {
    public IList<IAttributeWithValue> Attributes { get; init; } = new List<IAttributeWithValue>();
    public IList<Effect> Effects { get; set; } = new List<Effect>();
    public IList<Trigger> Triggers { get; set; } = new List<Trigger>();
}
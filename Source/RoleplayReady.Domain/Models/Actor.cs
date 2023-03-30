namespace RoleplayReady.Domain.Models;

public record Actor : Element, IHasAttributes {
    public IList<Bundle> Equipment { get; init; } = new List<Bundle>();
    public IList<Power> Powers { get; init; } = new List<Power>();
    public IList<ActorAction> Actions { get; init; } = new List<ActorAction>();
    public IList<Condition> Conditions { get; init; } = new List<Condition>();
    public IList<Entry> Entries { get; init; } = new List<Entry>();

    public IList<IAttributeWithValue> Attributes { get; init; } = new List<IAttributeWithValue>();
}
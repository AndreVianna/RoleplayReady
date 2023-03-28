namespace RoleplayReady.Domain.Models;

public record Entity : Element
{
    public IList<Set> Equipment { get; init; } = new List<Set>();
    public IList<Power> Powers { get; init; } = new List<Power>();
    public IList<EntityAction> Actions { get; init; } = new List<EntityAction>();
    public IList<Condition> Conditions { get; init; } = new List<Condition>();

    public IList<Modifier> Modifiers { get; init; } = new List<Modifier>();
    public IList<Entry> Entries { get; init; } = new List<Entry>();
}
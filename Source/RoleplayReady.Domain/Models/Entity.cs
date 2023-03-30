namespace RoleplayReady.Domain.Models;

public record Entity : Element, IHasAttributes, IHasValidations
{
    public IList<Set> Equipment { get; init; } = new List<Set>();
    public IList<Power> Powers { get; init; } = new List<Power>();
    public IList<EntityAction> Actions { get; init; } = new List<EntityAction>();
    public IList<Condition> Conditions { get; init; } = new List<Condition>();
    public IList<Entry> Entries { get; init; } = new List<Entry>();

    public IList<IElementAttribute> Attributes { get; init; } = new List<IElementAttribute>();
    public IList<EntityValidation> Validations { get; init; } = new List<EntityValidation>();
}
using RolePlayReady.Utilities.Contracts;

namespace RolePlayReady.Models;

public record RuleSet : Component, IRuleSet {
    public RuleSet() {
    }

    [SetsRequiredMembers]
    public RuleSet(string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(null, abbreviation, name, description, dateTime) { }

    public IList<ISource> Sources { get; init; } = new List<ISource>();

    public override TSelf CloneUnder<TSelf>(IEntity? _) {
        var result = base.CloneUnder<RuleSet>(null) with {
            Sources = new List<ISource>(Sources)
        };
        return (result as TSelf)!;
    }

}
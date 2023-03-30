using RoleplayReady.Domain.RuleSets;

namespace RoleplayReady.DataAccess;

public class InMemoryDataContext
{
    public Dictionary<string, RuleSet> RuleSets { get; } = new();

    public InMemoryDataContext()
    {
        InitializeDataContext();
    }

    private void InitializeDataContext()
    {
        // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
        var dnd5e = DnD5eFactory.Create();
        RuleSets.Add(dnd5e.Abbreviation, DnD5eFactory.Create());
    }
}
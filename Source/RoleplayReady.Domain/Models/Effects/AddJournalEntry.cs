namespace RoleplayReady.Domain.Models.Effects;

public record AddJournalEntry : Effect {
    [SetsRequiredMembers]
    public AddJournalEntry(EntrySection section, string title, string text)
        : base(e => {
            if (e is not Actor a) return e;
            a.Journal.Add(new Entry(section, title, text));
            return e;
        }) { }
}
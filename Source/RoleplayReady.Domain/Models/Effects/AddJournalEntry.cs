namespace RoleplayReady.Domain.Models.Effects;

public record AddJournalEntry : Effect {
    public AddJournalEntry(IHasEffects parent, EntryType type, string title, string text) : base(parent,
        e => {
            e.Entries.Add(new() { Type = type, Title = title, Text = text });
            return e;
        }) { }
}
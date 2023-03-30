namespace RoleplayReady.Domain.Models.Modifiers;

public record AddJournalEntry : Modifier {
    public AddJournalEntry(IHasModifiers target, EntryType type, string title, string text) : base(target,
        e => {
            e.Entries.Add(new() { Type = type, Title = title, Text = text });
            return e;
        }) { }
}
namespace RoleplayReady.Domain.Models;

public record JournalEntry : IJournalEntry {
    public JournalEntry() {

    }

    [SetsRequiredMembers]
    public JournalEntry(IEntity parent, EntrySection section, string title, string text, State? state = null) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Section = section;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Text = text ?? throw new ArgumentNullException(nameof(text));
        State = state ?? State.NotReady;
    }

    public required IEntity Parent { get; init; }
    public required EntrySection Section { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public State State { get; init; } = State.NotReady;
}
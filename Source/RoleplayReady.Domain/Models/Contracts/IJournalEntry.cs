namespace RoleplayReady.Domain.Models.Contracts;

public interface IJournalEntry : IAmTrackable {
    EntrySection Section { get; init; }
    string Title { get; init; }
    string Text { get; init; }
}
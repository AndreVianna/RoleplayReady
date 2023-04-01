namespace RoleplayReady.Domain.Models.Contracts;

public interface IJournalEntry : IAmTracked {
    EntrySection Section { get; init; }
    string Title { get; init; }
    string Text { get; init; }
}
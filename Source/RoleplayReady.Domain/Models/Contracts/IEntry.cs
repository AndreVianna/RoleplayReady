namespace RoleplayReady.Domain.Models.Contracts;

public interface IEntry {
    EntrySection Section { get; init; }
    string Title { get; init; }
    string Text { get; init; }

    DateTime Timestamp { get; init; }
    bool IsDeleted { get; init; }
}
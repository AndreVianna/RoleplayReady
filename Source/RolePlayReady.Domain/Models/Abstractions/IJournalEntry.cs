namespace RolePlayReady.Models.Abstractions;

public interface IJournalEntry {
    string Section { get; }
    string Title { get; }
    string Text { get; }
}
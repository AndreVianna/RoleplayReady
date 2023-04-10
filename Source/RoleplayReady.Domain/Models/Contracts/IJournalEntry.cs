namespace RolePlayReady.Models.Contracts;

public interface IJournalEntry {
    string Section { get; }
    string Title { get; }
    string Text { get; }
}
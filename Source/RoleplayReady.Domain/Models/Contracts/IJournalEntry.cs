namespace RolePlayReady.Models.Contracts;

public interface IJournalEntry : IVersion {
    string Section { get; init; }
    string Title { get; init; }
    string Text { get; init; }
}
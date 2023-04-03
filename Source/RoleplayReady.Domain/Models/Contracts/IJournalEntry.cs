namespace RolePlayReady.Models.Contracts;

public interface IJournalEntry : IVersion {
    string Section { get; init; }
    string Title { get; set; }
    string Text { get; set; }
}
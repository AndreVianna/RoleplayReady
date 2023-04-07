namespace RolePlayReady.Models.Contracts;

public interface IJournalEntry : ITrackable {
    string Section { get; init; }
    string Title { get; init; }
    string Text { get; init; }
}
namespace RolePlayReady.Models.Contracts;

public interface IIdentification {
    string Type { get; init; }
    string Abbreviation { get; init; }
    string Name { get; init; }
    string Description { get; init; }
    ISource? Source { get; init; }
    string FullName => $"<{Type}> {Name} ({Abbreviation})";
}
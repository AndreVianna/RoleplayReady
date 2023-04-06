namespace RolePlayReady.Models.Contracts;

public interface IIdentification {
    string Type { get; }
    string Abbreviation { get; init; }
    string Name { get; init; }
    string Description { get; init; }
    string FullName => $"<{Type}> {Name} ({Abbreviation})";
}
namespace RolePlayReady.Models.Contracts;

public interface IIdentification {
    string EntityType { get; }
    string Abbreviation { get; init; }
    string Name { get; init; }
    string Description { get; init; }
    string FullName { get; }
}
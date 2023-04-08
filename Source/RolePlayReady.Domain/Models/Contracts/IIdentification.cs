namespace RolePlayReady.Models.Contracts;

public interface IIdentification {
    string Name { get; }
    string? ShortName { get; }
    string Description { get; }
    string FullName { get; }
}
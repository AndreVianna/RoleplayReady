namespace RolePlayReady.Models.Abstractions;

public interface IBase : IValidatable {
    string Name { get; }
    string? ShortName { get; }
    string Description { get; }
    ICollection<string> Tags { get; }
}
namespace RolePlayReady.Models.Abstractions;

public interface IDescribed {
    string Name { get; }
    string Description { get; }
    string? ShortName { get; }
}
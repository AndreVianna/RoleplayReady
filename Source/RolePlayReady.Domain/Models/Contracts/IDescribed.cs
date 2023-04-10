namespace RolePlayReady.Models.Contracts;

public interface IDescribed {
    string Name { get; }
    string Description { get; }
    string? ShortName { get; }
}
namespace RoleplayReady.Domain.Models.Contracts;

public interface IAttribute {
    string Abbreviation { get; init; }
    string Name { get; init; }
    string Description { get; init; }
}

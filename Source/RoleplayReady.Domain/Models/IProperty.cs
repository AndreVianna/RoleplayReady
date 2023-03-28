namespace RoleplayReady.Domain.Models;

public interface IProperty
{
    System System { get; }
    string Name { get; }
    string Description { get; }
}